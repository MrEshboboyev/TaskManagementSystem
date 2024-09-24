using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Services.Interfaces;

public interface ITaskService
{
    // Create a new task
    Task<ResponseDTO<TaskDTO>> CreateTaskAsync(CreateTaskDTO createTaskDTO);

    // Update an existing task
    Task<ResponseDTO<TaskDTO>> UpdateTaskAsync(int taskId, UpdateTaskDTO updateTaskDTO);

    // Delete a task
    Task<ResponseDTO<bool>> DeleteTaskAsync(DeleteTaskDTO deleteTaskDTO);

    // Get a task by its ID
    Task<ResponseDTO<TaskDTO>> GetTaskByIdAsync(int taskId);

    // Get all tasks assigned to a specific user
    Task<ResponseDTO<IEnumerable<TaskDTO>>> GetTasksByUserIdAsync(string userId);

    // Get all tasks associated with a project
    Task<ResponseDTO<IEnumerable<TaskDTO>>> GetTasksByProjectIdAsync(int projectId);

    // Update the status of a task
    Task<ResponseDTO<TaskDTO>> UpdateTaskStatusAsync(int taskId, TaskStatusDTO statusDto);

    // Assign a task to a user
    Task<ResponseDTO<TaskDTO>> AssignTaskToUserAsync(int taskId, string userId);
}

