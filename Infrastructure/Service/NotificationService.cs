using Application.Dtos.common;
using Application.Dtos.Notification;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
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
        private readonly IMapper _mapper;

        public NotificationService(IAppRepository<Notification> _rep ,IMapper _map) 
        {
             _appRepository = _rep ;
            _mapper = _map ;
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
    }
}
