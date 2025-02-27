using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Dtos.Request;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(UserRegistrationDto userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            if (result.IsSuccess)
                return Ok(result);

            return Conflict(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            if (result.IsSuccess)
                return Ok(result);

            return NotFound(result);
        }
    }
}
