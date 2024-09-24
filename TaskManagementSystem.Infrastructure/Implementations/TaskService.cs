using AutoMapper;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Implementations;

public class TaskService(IUnitOfWork unitOfWork, IMapper mapper) : ITaskService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;


    public async Task<ResponseDTO<IEnumerable<TaskDTO>>> GetTasksByProjectIdAsync(int projectId)
    {
        try
        {
            // getting tasks by project
            var tasksFromDb = await _unitOfWork.TaskItem.GetAllAsync(
                filter: t => t.ProjectId.Equals(projectId),
                includeProperties: "AssignedUser,Project"
                );

            // mapping and return
            return new ResponseDTO<IEnumerable<TaskDTO>>(
                _mapper.Map<IEnumerable<TaskDTO>>(tasksFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<TaskDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<TaskDTO>>> GetTasksByUserIdAsync(string userId)
    {
        try
        {
            // getting tasks by user
            var tasksFromDb = await _unitOfWork.TaskItem.GetAllAsync(
                filter: t => t.AssignedUserId.Equals(userId),
                includeProperties: "AssignedUser,Project"
                );

            // mapping and return
            return new ResponseDTO<IEnumerable<TaskDTO>>(
                _mapper.Map<IEnumerable<TaskDTO>>(tasksFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<TaskDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<TaskDTO>> GetTaskByIdAsync(int taskId)
    {
        try
        {
            // get task by taskId
            var taskFromDb = await _unitOfWork.TaskItem.GetAsync(
                filter: t => t.Id.Equals(taskId),
                includeProperties: "AssignedUser,Project"
                );

            // mapping and return
            return new ResponseDTO<TaskDTO>(
                _mapper.Map<TaskDTO>(taskFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<TaskDTO>(ex.Message);
        }
    }


    public async Task<ResponseDTO<TaskDTO>> CreateTaskAsync(CreateTaskDTO createTaskDTO)
    {
        try
        {
            // getting project with this user
            var projectFromDb = await _unitOfWork.Project.GetAsync(
                p => p.Id.Equals(createTaskDTO.ProjectId) &&
                p.ManagerUserId.Equals(createTaskDTO.ManagerUserId)
                ) ?? throw new Exception("Project not found!");

            // prepare for this user
            var taskForDb = _mapper.Map<TaskItem>(createTaskDTO);

            // adding and save db
            await _unitOfWork.TaskItem.AddAsync(taskForDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<TaskDTO>(
                _mapper.Map<TaskDTO>(taskForDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<TaskDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<TaskDTO>> UpdateTaskAsync(int taskId, UpdateTaskDTO updateTaskDTO)
    {
        try
        {
            // getting project with this user
            var projectFromDb = await _unitOfWork.Project.GetAsync(
                p => p.Id.Equals(updateTaskDTO.ProjectId) &&
                p.ManagerUserId.Equals(updateTaskDTO.ManagerUserId)
                ) ?? throw new Exception("Project not found!");

            // getting this task with this project
            var taskFromDb = await _unitOfWork.TaskItem.GetAsync(
                filter: p => p.ProjectId.Equals(projectFromDb.Id) &&
                p.Id.Equals(taskId),
                includeProperties: "AssignedUser,Project"
                ) ?? throw new Exception("Task not found!");

            // prepare for this task
            _mapper.Map(updateTaskDTO, taskFromDb);

            // update and save db
            await _unitOfWork.TaskItem.Update(taskFromDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<TaskDTO>(
                _mapper.Map<TaskDTO>(taskFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<TaskDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<TaskDTO>> AssignTaskToUserAsync(int taskId, string userId)
    {
        try
        {
            // get this task
            var taskFromDb = await _unitOfWork.TaskItem.GetAsync(
                filter: p => p.Id.Equals(taskId),
                includeProperties: "AssignedUser,Project"
                ) ?? throw new Exception("Task not found!");

            // update fields
            taskFromDb.AssignedUserId = userId;

            // update and save
            await _unitOfWork.TaskItem.Update(taskFromDb);
            await _unitOfWork.SaveAsync();

            // returning updated task
            return new ResponseDTO<TaskDTO>(
                _mapper.Map<TaskDTO>(taskFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<TaskDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<TaskDTO>> UpdateTaskStatusAsync(int taskId, TaskStatusDTO taskStatusDTO)
    {
        try
        {
            // getting project with this user
            var projectFromDb = await _unitOfWork.Project.GetAsync(
                p => p.Id.Equals(taskStatusDTO.ProjectId) &&
                p.ManagerUserId.Equals(taskStatusDTO.ManagerUserId)
                ) ?? throw new Exception("Project not found!");

            // getting this task with this project
            var taskFromDb = await _unitOfWork.TaskItem.GetAsync(
                filter: p => p.ProjectId.Equals(projectFromDb.Id) &&
                p.Id.Equals(taskId),
                includeProperties: "AssignedUser,Project"
                ) ?? throw new Exception("Task not found!");

            // prepare task for db
            taskFromDb.Status = taskStatusDTO.Status;

            // update and save this
            await _unitOfWork.TaskItem.Update(taskFromDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<TaskDTO>(
                _mapper.Map<TaskDTO>(taskFromDb)
                );
        }
        catch (Exception ex)
        {
            return new ResponseDTO<TaskDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> DeleteTaskAsync(DeleteTaskDTO deleteTaskDTO)
    {
        try
        {
            // getting project with this user
            var projectFromDb = await _unitOfWork.Project.GetAsync(
                p => p.Id.Equals(deleteTaskDTO.ProjectId) &&
                p.ManagerUserId.Equals(deleteTaskDTO.ManagerUserId)
                ) ?? throw new Exception("Project not found!");

            // getting this task with this project
            var taskFromDb = await _unitOfWork.TaskItem.GetAsync(
                p => p.ProjectId.Equals(projectFromDb.Id) &&
                p.Id.Equals(deleteTaskDTO.TaskId)
                ) ?? throw new Exception("Task not found!");

            // remove and save db
            await _unitOfWork.TaskItem.RemoveAsync(taskFromDb);
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

