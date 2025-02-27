using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Dtos.Request;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Register(UserRegistrationDto userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            if (result.IsSuccess)
                return Ok(result);

            return Conflict(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            var result = await _userService.LoginUserAsync(userDto);
            if (result.IsSuccess)
                return Ok(result);

            return Unauthorized(result);
        }
    }
}
