using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class CompanyRepository(AppDbContext db) : Repository<Company>(db), ICompanyRepository
{
    private readonly AppDbContext _db = db;

    public async Task Update(Company company)
    {
        _db.Companies.Update(company);
    }
}
