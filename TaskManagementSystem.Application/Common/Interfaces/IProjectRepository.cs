using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Common.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
     Task Update(Project project);  
}

