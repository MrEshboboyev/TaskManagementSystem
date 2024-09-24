using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Comment

            // Comment -> CommentDTO
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.TaskTitle, opt => opt.MapFrom(src => src.Task.Title))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName));
            
            // CommentCreateDTO -> Comment
            CreateMap<CommentCreateDTO, Comment>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // CommentUpdateDTO -> Comment
            CreateMap<CommentUpdateDTO, Comment>();

            #endregion

            #region Notification

            // NotificationCreateDTO -> Notification
            CreateMap<NotificationCreateDTO, Notification>();

            #endregion

            #region Project

            // Project -> ProjectDTO
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.FullName));

            // TaskItem -> ProjectTaskDTO
            CreateMap<TaskItem, ProjectTaskDTO>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id));

            // ProjectCreateDTO -> Project
            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TaskStatusEnum.Todo));

            // ProjectUpdateDTO -> Project
            CreateMap<ProjectUpdateDTO, Project>();
            #endregion

            #region TaskItem

            // TaskItem -> TaskDTO
            CreateMap<TaskItem, TaskDTO>()
                .ForMember(dest => dest.AssignedUser, opt => opt.MapFrom(src => src.AssignedUser.FullName))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));

            // CreateTaskDTO -> TaskItem
            CreateMap<CreateTaskDTO, TaskItem>()
                .ForMember(dest => dest.Project.ManagerUserId, opt => opt.MapFrom(src => src.ManagerUserId));

            // UpdateTaskDTO -> TaskItem
            CreateMap<UpdateTaskDTO, TaskItem>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            #endregion
        }
    }
}
