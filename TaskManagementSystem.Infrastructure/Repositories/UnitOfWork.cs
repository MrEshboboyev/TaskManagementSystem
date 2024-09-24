using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        private readonly AppDbContext _db = db;

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
