using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        public ICommentRepository Comment { get; private set; } = new CommentRepository(db);
        public INotificationRepository Notification { get; private set; } = new NotificationRepository(db);
        public IProjectRepository Project { get; private set; } = new ProjectRepository(db);
        public ITaskItemRepository TaskItem { get; private set; } = new TaskItemRepository(db);

        private readonly AppDbContext _db = db;

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
