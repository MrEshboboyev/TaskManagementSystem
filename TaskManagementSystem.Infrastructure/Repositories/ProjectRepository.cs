using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class ProjectRepository(AppDbContext db) : Repository<Project>(db), IProjectRepository
{
    private readonly AppDbContext _db = db;

    public async Task Update(Project project)
    {
        _db.Projects.Update(project);
    }
}
