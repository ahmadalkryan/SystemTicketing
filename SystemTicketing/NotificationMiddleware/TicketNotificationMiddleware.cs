using Application.Dtos.Ticket;
using Application.Dtos.TicketTraceDto;
using Application.IService;
using Application.Serializer;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class TicketNotificationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly INotificationService _notificationService;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<TicketNotificationMiddleware> _logger;

    public TicketNotificationMiddleware(
        RequestDelegate next,
        INotificationService notificationService,
        AppDbContext dbContext,
        ILogger<TicketNotificationMiddleware> logger)
    {
        _next = next;
        _notificationService = notificationService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        
        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

       
        if (context.Request.Path.StartsWithSegments("/api/Ticket")||context.Request.Path.StartsWithSegments("/api/TicketTrace"))
        {
            //  إعادة تعيين تيار الاستجابة للقراءة
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseContent = await new StreamReader(responseBody).ReadToEndAsync();

            try
            {
               
                if (context.Request.Path.Value.EndsWith("/InsertTicket") &&
                    context.Request.Method == "POST" &&
                    context.Response.StatusCode == StatusCodes.Status200OK)
                {
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<TicketDto>>(
                        responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    if (apiResponse?.Result == true && apiResponse.Data != null)
                    {
                        await HandleNewTicket(apiResponse.Data);
                    }
                }
              
                else if (context.Request.Path.Value.EndsWith("/InsertTicketTrace") &&
                         context.Request.Method == "PUT" &&
                         context.Response.StatusCode == StatusCodes.Status200OK)
                {
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<TicketTraceDto>>(
                        responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    if (apiResponse?.Result == true && apiResponse.Data != null)
                    {
                        await HandleTicketUpdate(apiResponse.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went Wrong");
            }
        }

        
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }

    
    private async Task HandleNewTicket(TicketDto ticketDto)
    {
        try
        {
            // 7. الحصول على مسؤولي الصيانة
            var maintenanceManagers = await _dbContext.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur._role)
                .Where(u => u.UserRoles.Any(ur => ur._role.Name == "MaintenanceEmployee"))
                .ToListAsync();

            // 8. إرسال إشعار لكل مسؤول
            foreach (var manager in maintenanceManagers)
            {
                await _notificationService.SendNotification(
                    manager.UserId,
                    $"تم إنشاء تذكرة جديدة: #{ticketDto.TicketNumber}",
                    ticketDto.Id
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "فشل في إرسال إشعارات المسؤولين");
        }
    }

    private async Task HandleTicketUpdate(TicketTraceDto ticketTraceDto)
    {
        try
        {
            // 9. الحصول على التذكرة الأصلية
            var originalTicket = await _dbContext.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == ticketTraceDto.TicketId);

            // 10. التحقق من تغيير حالة التذكرة
            if (originalTicket != null && originalTicket.TicketStatusId != ticketTraceDto.StatusID)
            {
                // 11. إرسال إشعار للموظف
                await _notificationService.SendNotification(
                    ticketTraceDto.UserId, // يجب إضافة هذا الحقل للنموذج
                    $"تم تحديث حالة تذكرتك #{originalTicket.TicketNumber}",
                    ticketTraceDto.Id
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "فشل في إرسال إشعارات تحديث الحالة");
        }
    }
}