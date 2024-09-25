using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.User;

namespace TaskManagementSystem.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDTO<IEnumerable<UserDTO>>> GetPMs();
    }
}
