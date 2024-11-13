using GrowthPath.AuthAPI.Models.DTO;

namespace GrowthPath.AuthAPI.Service
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string rolename);
    }
}
