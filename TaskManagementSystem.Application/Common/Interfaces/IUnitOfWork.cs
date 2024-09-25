namespace TaskManagementSystem.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ICommentRepository Comment { get; }
        INotificationRepository Notification { get; }
        IProjectRepository Project { get; }
        ITaskItemRepository TaskItem { get; }
        ICompanyRepository Company { get; }

        Task SaveAsync();
    }
}
