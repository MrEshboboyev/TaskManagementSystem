using AutoMapper;
using TaskManagementSystem.Application.DTOs.Comment;
using TaskManagementSystem.Application.DTOs.Company;
using TaskManagementSystem.Application.DTOs.Notification;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Application.DTOs.Task;
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
            CreateMap<NotificationCreateDTO, Notification>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => NotificationStatus.Pending))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => false));

            // Notification => NotificationDTO
            CreateMap<Notification, NotificationDTO>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.FullName))
                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(src => src.Recipient.FullName));
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
            CreateMap<TaskCreateDTO, TaskItem>();

            // UpdateTaskDTO -> TaskItem
            CreateMap<TaskUpdateDTO, TaskItem>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            #endregion

            #region Company

            // Company -> CompanyDTO
            CreateMap<Company, CompanyDTO>()
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.FullName));

            // CompanyCreateDTO -> Company
            CreateMap<CompanyCreateDTO, Company>();

            // CompanyUpdateDTO -> Company
            CreateMap<CompanyUpdateDTO, Company>();
            #endregion
        }
    }
}
