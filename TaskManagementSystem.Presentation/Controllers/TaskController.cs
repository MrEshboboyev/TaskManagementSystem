using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Services.Interfaces;

namespace TaskManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskService TaskService) : ControllerBase
    {
        private readonly ITaskService _TaskService = TaskService;

        #region Private Methods

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new Exception("Login required!");
        #endregion

        [AllowAnonymous]
        [HttpGet("get-tasks-by-project-id")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            var result = await _TaskService.GetTasksByProjectIdAsync(projectId);

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("get-tasks-by-user-id")]
        public async Task<IActionResult> GetTasksByUserId()
        {
            var result = await _TaskService.GetTasksByUserIdAsync(GetUserId());

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("get-task-by-id")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var result = await _TaskService.GetTaskByIdAsync(taskId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskModel createTaskModel)
        {
            // prepare
            CreateTaskDTO createTaskDTO = new()
            {
                Title = createTaskModel.Title,
                ProjectId = createTaskModel.ProjectId,
                AssignedUserId = createTaskModel.AssignedUserId,
                Deadline = createTaskModel.Deadline,
                Description = createTaskModel.Description,
                Priority = createTaskModel.Priority,
                ManagerUserId = GetUserId()
            };

            var result = await _TaskService.CreateTaskAsync(createTaskDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("update-task")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UpdateTaskModel updateTaskModel)
        {
            // prepare
            UpdateTaskDTO updateTaskDTO = new()
            {
                Title = updateTaskModel.Title,
                ProjectId = updateTaskModel.ProjectId,
                AssignedUserId = updateTaskModel.AssignedUserId,
                Deadline = updateTaskModel.Deadline,
                Description = updateTaskModel.Description,
                Priority = updateTaskModel.Priority,
                ManagerUserId = GetUserId()
            };

            var result = await _TaskService.UpdateTaskAsync(taskId, updateTaskDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("delete-Task")]
        public async Task<IActionResult> DeleteTask([FromBody] DeleteTaskModel deleteTaskModel)
        {
            // prepare
            DeleteTaskDTO deleteTaskDTO = new()
            {
                ProjectId = deleteTaskModel.ProjectId,
                TaskId = deleteTaskModel.TaskId,
                ManagerUserId = GetUserId()
            };

            var result = await _TaskService.DeleteTaskAsync(deleteTaskDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("update-task-status")]
        public async Task<IActionResult> UpdateTaskStatus(int taskId, [FromBody] TaskStatusModel taskStatusModel)
        {
            TaskStatusDTO taskStatusDTO = new()
            {
                Status = taskStatusModel.Status,
                ProjectId = taskStatusModel.ProjectId,
                ManagerUserId = GetUserId()
            };

            var result = await _TaskService.UpdateTaskStatusAsync(taskId, taskStatusDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
        
        [HttpPut("assign-task-to-user")]
        public async Task<IActionResult> AssignTaskToUser(int taskId, string userId)
        {
            var result = await _TaskService.AssignTaskToUserAsync(taskId, userId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
