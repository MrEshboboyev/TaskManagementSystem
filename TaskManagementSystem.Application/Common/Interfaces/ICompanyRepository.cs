using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Common.Interfaces;

public interface ICompanyRepository : IRepository<Company>
{
     Task Update(Company company);  
}

