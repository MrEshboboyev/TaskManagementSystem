using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.Common.Utility;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.User;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Implementations
{
    public class UserService(UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseDTO<IEnumerable<UserDTO>>> GetPMs()
        {
            try
            {
                var allPMs = await _userManager.GetUsersInRoleAsync(SD.Role_PM);

                var mappedPMs = _mapper.Map<IEnumerable<UserDTO>>(allPMs);

                return new ResponseDTO<IEnumerable<UserDTO>>(mappedPMs);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<UserDTO>>(ex.Message);
            }
        }
    }
}
