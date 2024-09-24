using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Services.Interfaces;

public interface ITaskService
{
    // Create a new task
    Task<TaskDto> CreateTaskAsync(CreateTaskDTO createTaskDTO);

    // Update an existing task
    Task<TaskDto> UpdateTaskAsync(int taskId, UpdateTaskDTO updateTaskDTO);

    // Delete a task
    Task<bool> DeleteTaskAsync(int taskId);

    // Get a task by its ID
    Task<TaskDto> GetTaskByIdAsync(int taskId);

    // Get all tasks assigned to a specific user
    Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(string userId);

    // Get all tasks associated with a project
    Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(int projectId);

    // Update the status of a task
    Task<TaskDto> UpdateTaskStatusAsync(int taskId, TaskStatusDTO statusDto);

    // Assign a task to a user
    Task<TaskDto> AssignTaskToUserAsync(int taskId, string userId);
}

