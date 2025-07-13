using Application.Dtos.common;
using Application.Dtos.Notification;
using Application.Dtos.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface INotificationService
    {

        Task<IEnumerable<NotificationDto>> GetAllNotifications();
        Task<NotificationDto> GetNotificationByID(BaseDto<int> dto);

        Task<NotificationDto> SendNotification(string userID, string message, int ticketId);

        Task<NotificationDto> CreateNotification(CreateNotificationDto createNotificationDto);

        Task<NotificationDto> UpdateNotification(UpdateNotificationDto updateNotificationDto );

        Task<NotificationDto> DeleteNotifiaction(BaseDto<int> dto);
        Task<bool> IsReadNotification(int id);
    }
}
