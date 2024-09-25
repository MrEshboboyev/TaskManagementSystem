using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Common.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
     Task Update(ApplicationUser applicationUser);  
}

