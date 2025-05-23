using Application.Dtos.Notification;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.Notifiction
{
    public class NotificationProfile:Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification,NotificationDto>();
            CreateMap<CreateNotificationDto,Notification>();
            CreateMap<UpdateNotificationDto,Notification>();
        }
    }
}
