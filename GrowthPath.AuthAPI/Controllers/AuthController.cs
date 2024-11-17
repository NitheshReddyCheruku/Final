using GrowthPath.AuthAPI.Models.DTO;
using GrowthPath.AuthAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrowthPath.AuthAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ResponseDto _response = new ResponseDto();
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
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
        // This ensures only authenticated users can access this endpoint
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _authService.GetAllUsersAsync();
                _response.Result = users;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving users: " + ex.Message;
                return StatusCode(500, _response);
            }
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var userDto = await _authService.GetUserByIdAsync(userId);

                if (userDto == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "User not found.";
                    return NotFound(_response);
                }

                _response.Result = userDto;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving user: " + ex.Message;
                return StatusCode(500, _response);
            }
        }
        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetUserById(string userId)
        //{
        //    // Assuming _authService has a method to fetch user by ID
        //    var user = await _authService.GetUserByIdAsync(userId);

        //    if (user == null)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = "User not found.";
        //        return NotFound(_response);
        //    }

        //    var userDto = new UserDto
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        Name = user.Name,
        //        PhoneNumber = user.PhoneNumber
        //    };

        //    _response.Result = userDto;
        //    return Ok(_response);
        //}
        //[HttpGet("employees")]
        //public async Task<IActionResult> GetEmployees()
        //{
        //    var employees = await _userService.GetUsersByRole("Employee");  // Fetching employees by role
        //    return Ok(employees);  // Returning the list of EmployeeDto objects
        //}
    }
}
