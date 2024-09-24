using AutoMapper;
using System.Linq.Expressions;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Implementations;

public class ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : IProjectService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;


    public async Task<ResponseDTO<IEnumerable<ProjectDTO>>> GetAllProjectsAsync(ProjectFilterDTO projectFilterDto = null)
    {
        try
        {
            // prepare filter
            // Build the filter expression based on the provided filter criteria
            Expression<Func<Project, bool>> filter = s =>
                (!projectFilterDto.Status.HasValue || s.Status == projectFilterDto.Status.Value) &&
                (!projectFilterDto.StartDateFrom.HasValue || s.StartDate >= projectFilterDto.StartDateFrom.Value) &&
                (!projectFilterDto.StartDateTo.HasValue || s.EndDate <= projectFilterDto.StartDateTo.Value);

            // getting all projects
            var projectsFromDb = await _unitOfWork.Project.GetAllAsync(
                filter, 
                includeProperties: "Manager"
                );

            // mapping and return
            return new ResponseDTO<IEnumerable<ProjectDTO>>(
                _mapper.Map<IEnumerable<ProjectDTO>>(projectsFromDb)  
                );
        }
        catch (Exception ex) 
        {
            return new ResponseDTO<IEnumerable<ProjectDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<ProjectTaskDTO>>> GetProjectTasksAsync(int projectId)
    {
        try
        {
            // getting all tasks for project
            var tasksFromDb = await _unitOfWork.TaskItem.GetAllAsync(
                t => t.ProjectId.Equals(projectId)
                );

            // mapping and return
            return new ResponseDTO<IEnumerable<ProjectTaskDTO>>(
                _mapper.Map<IEnumerable<ProjectTaskDTO>>(tasksFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<ProjectTaskDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<ProjectDTO>> GetProjectByIdAsync(int projectId)
    {
        try
        {
            // get this project
            var projectFromDb = await _unitOfWork.Project.GetAsync(
                filter: p => p.Id.Equals(projectId),
                includeProperties: "Manager"
                );

            // mapping and return
            return new ResponseDTO<ProjectDTO>(
                _mapper.Map<ProjectDTO>(projectFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ProjectDTO>(ex.Message);
        }
    }


    public async Task<ResponseDTO<ProjectDTO>> CreateProjectAsync(ProjectCreateDTO projectCreateDTO)
    {
        try
        {
            // prepare for db
            var projectForDb = _mapper.Map<Project>(projectCreateDTO);

            // adding and save
            await _unitOfWork.Project.AddAsync(projectForDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<ProjectDTO>(
                _mapper.Map<ProjectDTO>(projectForDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ProjectDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<ProjectDTO>> UpdateProjectAsync(int projectId, ProjectUpdateDTO projectUpdateDTO)
    {
        try
        {
            // get project
            var projectFromDb = await _unitOfWork.Project.GetAsync(
                filter: p => p.Id.Equals(projectId) && 
                p.ManagerUserId.Equals(projectUpdateDTO.ManagerUserId),
                includeProperties: "Manager"
                ) ?? throw new Exception("Project not found!");

            // mapping for db
            _mapper.Map(projectUpdateDTO, projectFromDb);

            // updating and save
            await _unitOfWork.Project.Update(projectFromDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<ProjectDTO>(
                _mapper.Map<ProjectDTO>(projectFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ProjectDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> DeleteProjectAsync(ProjectDeleteDTO projectDeleteDTO)
    {
        try
        {
            // get project
            var projectFromDb = await _unitOfWork.Project.GetAsync(
                filter: p => p.Id.Equals(projectDeleteDTO.ProjectId) &&
                p.ManagerUserId.Equals(projectDeleteDTO.ManagerUserId),
                includeProperties: "Manager"
                ) ?? throw new Exception("Project not found!");

            // removing and save
            await _unitOfWork.Project.RemoveAsync(projectFromDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<bool>(
                _mapper.Map<bool>(true)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> AssignProjectManagerAsync(int projectId, string userId)
    {
        try
        {
            // get project
            var projectFromDb = await _unitOfWork.Project.GetAsync(
                filter: p => p.Id.Equals(projectId),
                includeProperties: "Manager"
                ) ?? throw new Exception("Project not found!");

            // update manager field
            projectFromDb.ManagerUserId = userId;

            // removing and save
            await _unitOfWork.Project.Update(projectFromDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<bool>(
                _mapper.Map<bool>(true)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }
}

