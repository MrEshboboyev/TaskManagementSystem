using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Services.Interfaces;

public interface IProjectService
{
    // Create a new project
    Task<ResponseDTO<ProjectDTO>> CreateProjectAsync(ProjectCreateDTO projectDto);

    // Update an existing project
    Task<ResponseDTO<ProjectDTO>> UpdateProjectAsync(int projectId, ProjectUpdateDTO projectDto);

    // Get project by ID
    Task<ResponseDTO<ProjectDTO>> GetProjectByIdAsync(int projectId);

    // Get all projects with optional filtering (e.g., by status)
    Task<ResponseDTO<IEnumerable<ProjectDTO>>> GetAllProjectsAsync(ProjectFilterDTO filterDto = null);

    // Get tasks for a project
    Task<ResponseDTO<IEnumerable<ProjectTaskDTO>>> GetProjectTasksAsync(int projectId);

    // Assign a user as the manager of a project
    Task<ResponseDTO<bool>> AssignProjectManagerAsync(int projectId, string userId);

    // Delete a project
    Task<ResponseDTO<bool>> DeleteProjectAsync(ProjectDeleteDTO projectDeleteDTO);
}

