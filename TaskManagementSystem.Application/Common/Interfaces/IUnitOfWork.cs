namespace TaskManagementSystem.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
