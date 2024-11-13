using GrowthPath.AuthAPI.Models.DTO;
using GrowthPath.AuthAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrowthPath.AuthAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ResponseDto _response = new ResponseDto();

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var result = await _authService.Login(model);
            if (result.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid credentials.";
                return BadRequest(_response);
            }
            _response.Result = result;
            return Ok(_response);
        }
        [AllowAnonymous]
        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var success = await _authService.AssignRole(model.Email, model.Role);
            if (!success)
            {
                _response.IsSuccess = false;
                _response.Message = "Role assignment failed.";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
