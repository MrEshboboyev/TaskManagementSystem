using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class NotificationRepository(AppDbContext db) : Repository<Notification>(db), INotificationRepository
{
    private readonly AppDbContext _db = db;

    public async Task Update(Notification notification)
    {
        _db.Notifications.Update(notification);
    }
}
