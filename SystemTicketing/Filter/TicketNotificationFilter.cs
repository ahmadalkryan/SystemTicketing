using Application.Dtos.Ticket;
using Application.Dtos.TicketTraceDto;
using Application.IService;
using Application.Serializer;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class TicketNotificationFilter : IAsyncActionFilter
{
    private readonly INotificationService _notificationService;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<TicketNotificationFilter> _logger;

    public TicketNotificationFilter(
        INotificationService notificationService,
        AppDbContext dbContext,
        ILogger<TicketNotificationFilter> logger)
    {
        _notificationService = notificationService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Execute the action
        var resultContext = await next();

        // Only handle successful responses
        if (resultContext.HttpContext.Response.StatusCode != StatusCodes.Status200OK)
            return;

        var actionName = context.ActionDescriptor.RouteValues["action"];
        var controllerName = context.ActionDescriptor.RouteValues["controller"];

        try
        {
            if (controllerName == "Ticket" && actionName == "InsertTicket")
            {
                if (resultContext.Result is ObjectResult objectResult &&
                    objectResult.Value is ApiResponse<TicketDto> apiResponse &&
                    apiResponse.Result &&
                    apiResponse.Data != null)
                {
                    await HandleNewTicket(apiResponse.Data);
                }
            }
            else if (controllerName == "TicketTrace" && actionName == "InsertTicketTrace")
            {
                if (resultContext.Result is ObjectResult objectResult &&
                    objectResult.Value is ApiResponse<TicketTraceDto> apiResponse &&
                    apiResponse.Result &&
                    apiResponse.Data != null)
                {
                    await HandleTicketUpdate(apiResponse.Data);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing notification");
        }
    }

    private async Task HandleNewTicket(TicketDto ticketDto)
    {
        var maintenanceManagers = await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur._role)
            .Where(u => u.UserRoles.Any(ur => ur._role.Name == "MaintenanceEmployee"))
            .ToListAsync();

        //var manger = await _dbContext.Users.Include(u => u.UserRoles.
        //Any(u => u._role.Name == "MaintenanceManager")).
        //    Select(u => new
        //    {
        //        u.Name,
        //        u.Email,
        //        u.UserId,
        //    }).ToListAsync();


        foreach (var manager in maintenanceManagers)
        {
            await _notificationService.SendNotification(
                manager.UserId,
                $"Ticket created successfuly : #{ticketDto.TicketNumber}",
                ticketDto.Id
            );
        }
    }

    private async Task HandleTicketUpdate(TicketTraceDto ticketTraceDto)
    {
        var originalTicket = await _dbContext.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == ticketTraceDto.TicketId);

        if (originalTicket != null && originalTicket.TicketStatusId != ticketTraceDto.StatusID)
        {
            await _notificationService.SendNotification(
                ticketTraceDto.UserId,
                $" Ticket updated successfuly   #{originalTicket.TicketNumber}",
                ticketTraceDto.Id
            );
        }
    }
}