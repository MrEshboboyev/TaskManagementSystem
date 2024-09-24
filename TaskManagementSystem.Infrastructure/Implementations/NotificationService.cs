using AutoMapper;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Implementations;

public class NotificationService(IUnitOfWork unitOfWork, IMapper mapper) : INotificationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;


    public async Task<ResponseDTO<IEnumerable<NotificationDTO>>> GetUnreadNotificationsAsync(string userId)
    {
        try
        {
            // getting notifications with IsRead is false
            var notificationsFromDb = await _unitOfWork.Notification.GetAllAsync(
                n => !n.IsRead && n.UserId.Equals(userId)
                );

            // mapping and return
            return new ResponseDTO<IEnumerable<NotificationDTO>>(
                _mapper.Map<IEnumerable<NotificationDTO>>(notificationsFromDb));
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<NotificationDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<NotificationDTO>>> GetUserNotificationsAsync(string userId)
    {
        try
        {
            // getting notifications for this user
            var notificationsFromDb = await _unitOfWork.Notification.GetAllAsync(
                n => n.UserId.Equals(userId)
                );

            // mapping and return
            return new ResponseDTO<IEnumerable<NotificationDTO>>(
                _mapper.Map<IEnumerable<NotificationDTO>>(notificationsFromDb));
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<NotificationDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<NotificationDTO>> SendNotificationAsync(NotificationCreateDTO notificationCreateDTO)
    {
        try
        {
            // prepare entity
            var notificationForDb = _mapper.Map<Notification>(notificationCreateDTO);

            // adding and save db
            await _unitOfWork.Notification.AddAsync(notificationForDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<NotificationDTO>(
                _mapper.Map<NotificationDTO>(notificationForDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<NotificationDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> MarkAsReadAsync(int notificationId)
    {
        try
        {
            // getting this notification
            var notificationFromDb = await _unitOfWork.Notification.GetAsync(
                n => n.Id.Equals(notificationId)
                ) ?? throw new Exception("Notification not found!");

            // field updated (IsRead = true)
            notificationFromDb.IsRead = true;

            // update and save
            await _unitOfWork.Notification.Update(notificationFromDb);
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
            // getting this notification
            var notificationFromDb = await _unitOfWork.Notification.GetAsync(
                n => n.Id.Equals(notificationId)
                ) ?? throw new Exception("Notification not found!");

            // remove and save
            await _unitOfWork.Notification.RemoveAsync(notificationFromDb);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO<bool>(true);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }
}

