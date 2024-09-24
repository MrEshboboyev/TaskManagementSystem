using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Common.Interfaces;

public interface ITaskItemRepository : IRepository<TaskItem>
{
     Task Update(TaskItem taskItem);  
}

