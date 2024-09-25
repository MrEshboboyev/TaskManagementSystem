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
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        #region Private Methods

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new Exception("Login required!");
        #endregion

        [AllowAnonymous]
        [HttpGet("get-all-projects")]
        public async Task<IActionResult> GetAllProjects([FromQuery] ProjectFilterDTO projectFilterDTO)
        {
            var result = await _projectService.GetAllProjectsAsync(projectFilterDTO);

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("get-project-tasks")]
        public async Task<IActionResult> GetProjectTasks(int projectId)
        {
            var result = await _projectService.GetProjectTasksAsync(projectId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("get-project-by-id")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var result = await _projectService.GetProjectByIdAsync(projectId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("create-project")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectModel createProjectModel)
        {
            // prepare
            ProjectCreateDTO projectCreateDTO = new()
            {
                Name = createProjectModel.Name,
                Description = createProjectModel.Description,
                StartDate = createProjectModel.StartDate,
                EndDate = createProjectModel.EndDate,
                ManagerUserId = GetUserId()
            };

            var result = await _projectService.CreateProjectAsync(projectCreateDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("update-project")]
        public async Task<IActionResult> UpdateProject(int projectId, [FromBody] UpdateProjectModel updateProjectModel)
        {
            // prepare
            ProjectUpdateDTO projectUpdateDTO = new()
            {
                Name = updateProjectModel.Name,
                Description = updateProjectModel.Description,
                StartDate = updateProjectModel.StartDate,
                EndDate = updateProjectModel.EndDate,
                ManagerUserId = GetUserId()
            };

            var result = await _projectService.UpdateProjectAsync(projectId, projectUpdateDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("delete-project")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            // prepare
            ProjectDeleteDTO projectDeleteDTO = new()
            {
                ManagerUserId = GetUserId(),
                ProjectId = projectId
            };

            var result = await _projectService.DeleteProjectAsync(projectDeleteDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("assign-project-manager")]
        public async Task<IActionResult> AssignProjectManager([FromBody] AssignProjectManagerModel assignProjectManagerModel)
        {
            var result = await _projectService.AssignProjectManagerAsync(assignProjectManagerModel.ProjectId, assignProjectManagerModel.ManagerId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
