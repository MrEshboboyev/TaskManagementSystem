using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Common.Interfaces;

public interface INotificationRepository : IRepository<Notification>
{
     Task Update(Notification notification);  
}

