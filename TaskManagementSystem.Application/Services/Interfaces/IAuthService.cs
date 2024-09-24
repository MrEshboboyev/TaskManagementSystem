using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO<string>> LoginAsync(LoginModel loginModel);
        Task<ResponseDTO<string>> RegisterAsync(RegisterModel registerModel);
        Task<ResponseDTO<string>> GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
