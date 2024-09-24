using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class TaskItemRepository(AppDbContext db) : Repository<TaskItem>(db), ITaskItemRepository
{
    private readonly AppDbContext _db = db;

    public async Task Update(TaskItem taskItem)
    {
        _db.Tasks.Update(taskItem);
    }
}
