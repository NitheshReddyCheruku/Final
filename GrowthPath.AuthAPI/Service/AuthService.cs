using GrowthPath.AuthAPI.Models.DTO;
using GrowthPath.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using GrowthPath.AuthAPI.Data;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace GrowthPath.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ResponseDto _response = new ResponseDto();

        public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManager,
                           IJwtTokenGenerator jwtTokenGenerator, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleManager = roleManager;
        }

        public async Task<string> Register(RegistrationRequestDto request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return null;
            }
            return result.Errors.FirstOrDefault()?.Description;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            }).ToList();

            return userDtos;
        }
       


        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByNameAsync(loginRequestDto.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
            {
                return new LoginResponseDto { User = null, Token = "", Role = "" };
            }

            // Generate the token and await the result
            var token = await _jwtTokenGenerator.GenerateToken(user); // Make sure to await this

            // Retrieve user role
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(); // Assuming single role per user

            // Populate UserDto with user's details
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            // Return the response with populated UserDto, Token, and Role
            return new LoginResponseDto
            {
                User = userDto,
                Token = token,
                Role = role
            };
        }


        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _response.Message = "User not found";
                return false;
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    _response.Message = "Role creation failed";
                    return false;
                }
            }

            var roleAssignmentResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleAssignmentResult.Succeeded)
            {
                _response.Message = "Role assignment to user failed";
                return false;
            }

            return true;
        }
      /*  public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            // Assume _userManager is a dependency injected UserManager<User> for user management
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }*/
        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
