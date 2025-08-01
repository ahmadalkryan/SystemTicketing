using Application.Dtos.Action;
using Application.Dtos.Ticket;
using Application.Dtos.TicketTraceDto;
using Application.IRepository;
using Application.IService;
using Application.Serializer;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class TicketNotificationFilter : IAsyncActionFilter
{
    private readonly INotificationService _notificationService;
    private readonly AppDbContext _dbContext;
    private readonly IAppRepository<User> _app;
    private readonly ILogger<TicketNotificationFilter> _logger;

    public TicketNotificationFilter(
        IAppRepository<User> app,
        INotificationService notificationService,
        AppDbContext dbContext,
        ILogger<TicketNotificationFilter> logger)
    {
        _notificationService = notificationService;
        _dbContext = dbContext;
        _logger = logger;
        _app=app;
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
                if(resultContext.Result is RawJsonActionResult rawJsonResult)
                {

                    var jsonString = rawJsonResult.Value();       
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<TicketDto>>(jsonString);

                    if (apiResponse != null && apiResponse.Result && apiResponse.Data != null)
                    {
                        await HandleNewTicket(apiResponse.Data);
                    }

                }
                    
                
            }
            else if (controllerName == "TicketTrace" && actionName == "InsertTicketTrace")
            {
                if (resultContext.Result is RawJsonActionResult rawJsonResult)
                {

                    var jsonString = rawJsonResult.Value();
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<TicketTraceDto>>(jsonString);

                    if (apiResponse != null && apiResponse.Result && apiResponse.Data != null)
                    {
                        await HandleTicketUpdate(apiResponse.Data);
                    }

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
      .Where(u => u.UserRoles.Any(ur => ur._role.Name == "Maintenance"))
                     .Select(u => u.UserId)     // get UserID
                              .ToListAsync();


        foreach (var manager in maintenanceManagers)
        {
            await _notificationService.SendNotification(
                manager,
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

        await _notificationService.SendNotification(
                ticketTraceDto.UserId,
                $" Ticket updated successfuly   #{originalTicket.TicketNumber}",
                ticketTraceDto.TicketId
            );

    }
}