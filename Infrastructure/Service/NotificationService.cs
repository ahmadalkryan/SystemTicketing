﻿using Application.Dtos.common;
using Application.Dtos.Notification;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IAppRepository<Notification> _appRepository;
        private readonly IAppRepository<User> _userRepository;
        private readonly IMapper _mapper;
       // private readonly IHubContext<NotificationHub> _hubContext;
        private readonly Application.IService.IEmailService _emailService;
        private readonly ILogger<Notification> _logger;
        //private readonly AppDbContext _app;

        public NotificationService(IAppRepository<Notification> _rep ,IMapper _map ,
            Application.IService.IEmailService emailService ,ILogger<Notification> logger,IAppRepository<User> appRepository ) 
        {
             _appRepository = _rep ;
            _mapper = _map ;
         ///   _hubContext = hubContext ;
            _emailService = emailService ;
            _logger = logger ;
           _userRepository = appRepository ;
        }



        public async Task<NotificationDto> CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var n = _mapper.Map<Notification>(createNotificationDto);

            await _appRepository.Insertasync(n);
            return _mapper.Map<NotificationDto>(n);
        }

        public async Task<NotificationDto> DeleteNotifiaction(BaseDto<int> dto)
        {
            var n =(await _appRepository.FindAsync(x=>x.Id == dto.Id)).FirstOrDefault();

            await _appRepository.RemoveAsync(n);
            return _mapper.Map<NotificationDto>(n) ;
        }

        public async Task<IEnumerable<NotificationDto>> GetAllNotifications()
        {
            return _mapper.Map<IEnumerable<NotificationDto>>(await _appRepository.GetAllAsync());
        }

        public async Task<NotificationDto> GetNotificationByID(BaseDto<int> dto)
        {
            var n= await _appRepository.GetById(dto.Id);

            return _mapper.Map<NotificationDto>(n);

        }

        public async Task<NotificationDto> UpdateNotification(UpdateNotificationDto updateNotificationDto)
        { 
            var n = _mapper.Map<Notification>(updateNotificationDto);

            await _appRepository.UpdateAsync(n);
            return _mapper.Map<NotificationDto>(n);
        }

         public async Task<bool> IsReadNotification(int id)
        {
            BaseDto<int> b = new BaseDto<int> { Id = id };
            var N = await GetNotificationByID(b);

            return N.IsRead ? true : false;
        }

       public async  Task<NotificationDto> SendNotification(string userID, string message, int ticketId)
        {
            CreateNotificationDto notificationDto = new CreateNotificationDto { UserID = userID, Message = message, TicketId = ticketId 
            ,IsRead=false,SentAt=DateTime.UtcNow

            
            };
            var n = _mapper.Map<Notification>(notificationDto);
            await CreateNotification(notificationDto);

            var NotDto = _mapper.Map<NotificationDto>(n);

            ////send Notification by SignalR
            //     await _hubContext.Clients
            //                  .User(userID)          // send to specific user 
            //                            .SendAsync("ReceiveNotification", NotDto);

            // send Email 
            try
            {
                // get Email For USER  ***************************
                //*********************
                //  Users.FirstOrDefault(x => x.UserId == userID);
                var users = _userRepository.GetAllAsync();
                 var user= users.Result.FirstOrDefault(x => x.UserId == userID);

                //  string userEmail = await _userService.GetUserEmailById(userID);

                string userEmail = user.Email;
                if (!string.IsNullOrEmpty(userEmail))
                {
                    string emailSubject = " NEW Notification  " + message;

                   await _emailService.SendEmailAsync(userEmail, emailSubject,message);
                }
            }
            catch (Exception ex)
            {
                // سجل الخطأ (logging) ولكن لا توقف التطبيق
                _logger.LogError(ex, "Failed to send email notification");
            }



            UpdateNotificationDto updatnotification = new UpdateNotificationDto
            {
                Id=n.Id,

                IsRead = true,
            };

            await UpdateNotification(updatnotification);





            return NotDto ;


        }

        
    }
}
