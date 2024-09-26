using AutoMapper;
using Microsoft.OpenApi.Writers;
using System.Runtime.InteropServices;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Notification;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Infrastructure.Implementations;

public class NotificationService(IUnitOfWork unitOfWork, IMapper mapper) : INotificationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseDTO<NotificationDTO>> CreateNotificationAsync(NotificationCreateDTO notificationCreateDTO)
    {
        try
        {
            // prepare notification
            var notificationForDb = _mapper.Map<Notification>(notificationCreateDTO);

            await _unitOfWork.Notification.AddAsync(notificationForDb);
            await _unitOfWork.SaveAsync();

            // getting this notification
            var notificationFromDb = await _unitOfWork.Notification.GetAsync(
                filter: n => n.Id.Equals(notificationForDb.Id),
                includeProperties: "Sender,Recipient"
                ) ?? throw new Exception("Notification not found!");

            // mapping return data
            var mappingNotificationDTO = _mapper.Map<NotificationDTO>(notificationFromDb);

            return new ResponseDTO<NotificationDTO>(mappingNotificationDTO);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<NotificationDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<NotificationDTO>>> GetNotificationsForUserAsync(string userId)
    {
        try
        {
            var userNotifications = await _unitOfWork.Notification.GetAllAsync(
                filter: n => n.RecipientId.Equals(userId),
                includeProperties: "Sender,Recipient"
                );

            var mappedNotifications = _mapper.Map<IEnumerable<NotificationDTO>>(userNotifications);

            return new ResponseDTO<IEnumerable<NotificationDTO>>(mappedNotifications);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<NotificationDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> MarkAsReadAsync(int notificationId)
    {
        try
        {
            var notificationFromDb = await _unitOfWork.Notification.GetAsync(
                filter: n => n.Id.Equals(notificationId)
                ) ?? throw new Exception("Notification not found!");

            notificationFromDb.IsRead = true;
            await _unitOfWork.Notification.Update(notificationFromDb);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO<bool>(true);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> RespondToNotificationAsync(int notificationId, NotificationResponseDTO responseDTO)
    {
        try
        {
            var notificationFromDb = await _unitOfWork.Notification.GetAsync(
                n => n.Id.Equals(notificationId)
                ) ?? throw new Exception("Notification not found!");

            notificationFromDb.Status = responseDTO.Status;
            notificationFromDb.ActionTakenAt = DateTime.UtcNow;

            await _unitOfWork.Notification.Update(notificationFromDb);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO<bool>(true);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }

    public async Task<ResponseDTO<NotificationDTO>> GetNotificationDetailsAsync(int notificationId)
    {
        try
        {
            // getting this notification
            var notificationFromDb = await _unitOfWork.Notification.GetAsync(
                filter: n => n.Id.Equals(notificationId),
                includeProperties: "Sender,Recipient"
                ) ?? throw new Exception("Notification not found!");

            // mapping return data
            var mappingNotificationDTO = _mapper.Map<NotificationDTO>(notificationFromDb);

            return new ResponseDTO<NotificationDTO>(mappingNotificationDTO);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<NotificationDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> NotifyPMStatusChangeAsync(string pmId, bool isAccepted)
    {
        try
        {
            var message = isAccepted ? "Your registration has been accepted." : "Your registration has been declined.";

            // create a notification
            Notification notificationForDb = new()
            {
                SenderId = null,
                RecipientId = pmId,
                Message = message,
                Type = NotificationType.PMApprovalResult,
                Status = isAccepted ? NotificationStatus.Accepted : NotificationStatus.Declined,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _unitOfWork.Notification.AddAsync(notificationForDb);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO<bool>(true);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> DeleteNotificationAsync(int notificationId)
    {
        try
        {
            var notificationFromDb = await _unitOfWork.Notification.GetAsync(
                n => n.Id.Equals(notificationId)
                ) ?? throw new Exception("Notification not found!");

            await _unitOfWork.Notification.RemoveAsync(notificationFromDb);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO<bool>(true);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<NotificationDTO>>> GetPendingNotificationsAsync(string userId)
    {
        try
        {
            var userPendingNotifications = await _unitOfWork.Notification.GetAllAsync(
                filter: n => n.RecipientId.Equals(userId) && n.Status == NotificationStatus.Pending,
                includeProperties: "Sender,Recipient"
                );

            var mappedNotificationDTOs = _mapper.Map<IEnumerable<NotificationDTO>>(userPendingNotifications);    

            return new ResponseDTO<IEnumerable<NotificationDTO>>(mappedNotificationDTOs);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<NotificationDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> MarkAllAsReadAsync(string userId)
    {
        try
        {
            var userUnreadNotifications = await _unitOfWork.Notification.GetAllAsync(
                n => n.RecipientId.Equals(userId) && !n.IsRead);

            foreach (var notification in userUnreadNotifications) 
                notification.IsRead = true;

            await _unitOfWork.SaveAsync();

            return new ResponseDTO<bool>(true);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }
}

