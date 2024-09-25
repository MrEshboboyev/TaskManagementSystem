using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : Repository<ApplicationUser>(db), IUserRepository
{
    private readonly AppDbContext _db = db;

    public async Task Update(ApplicationUser applicationUser)
    {
        _db.Users.Update(applicationUser);
    }
}
