using GrowthPath.AuthAPI.Models;
using GrowthPath.AuthAPI.Models.DTO;

namespace GrowthPath.AuthAPI.Service
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string rolename);
        //Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<List<UserDto>> GetAllUsersAsync(); // New method
        Task<UserDto> GetUserByIdAsync(string userId);





    }
}
