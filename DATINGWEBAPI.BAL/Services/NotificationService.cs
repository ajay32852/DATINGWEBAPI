using AutoMapper;
using DATINGWEBAPI.BAL.Hubs;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.DAL.Entities;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace DATINGWEBAPI.BAL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<NotificationService> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationHubClient _hubClient;
        public NotificationService(
            IMapper mapper,
            IStringLocalizer<NotificationService> localizer,
            IConfiguration configuration,
            INotificationRepository notificationRepository,
            IHttpContextAccessor httpContextAccessor,
            INotificationHubClient hubClient
        )
        {
            _mapper = mapper;
            _localizer = localizer;
            _configuration = configuration;
            _notificationRepository = notificationRepository;
            _httpContextAccessor = httpContextAccessor;
            _hubClient = hubClient;
        }

        public async Task<NotificationDTO> CreateNotificationAsync(long userId, NotificationRequestDTO dto)
        {
            // Map incoming DTO to Entity
            var notification = _mapper.Map<NOTIFICATION>(dto);
            notification.USERID = userId;
            notification.ISREAD = false;
            notification.CREATEDAT = DateTime.UtcNow;
            // Persist to DB using DAL
            var createdNotification = await _notificationRepository.CreateNotificationAsync(userId, notification);
            // Push real-time notification via SignalR
            await _hubClient.SendNotificationAsync(userId, new
            {
                NotificationId = createdNotification.NOTIFICATIONID,
                Title = createdNotification.TITLE,
                Message = createdNotification.MESSAGE,
                Type = createdNotification.TYPE,
                CreatedAt = createdNotification.CREATEDAT
            });
            // Return mapped DTO
            return _mapper.Map<NotificationDTO>(createdNotification);
        }


        public async Task<List<NotificationDTO>> GetUserNotificationsAsync(long userId)
        {
            var notifications = await _notificationRepository.GetUserNotificationsAsync(userId);
            return _mapper.Map<List<NotificationDTO>>(notifications);
        }

        public async Task<bool> MarkAsReadAsync(long userId, long notificationId)
        {
            return await _notificationRepository.MarkAsReadAsync(userId, notificationId);
        }
    }
}
