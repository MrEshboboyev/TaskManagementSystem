using AutoMapper;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Entities;

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
        }
    }
}
