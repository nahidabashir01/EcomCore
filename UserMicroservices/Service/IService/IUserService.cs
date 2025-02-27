using Microsoft.AspNetCore.Identity.Data;
using ServiceRespnse.Models;
using UserMicroservice.Dtos.Request;
using UserMicroservice.Models;

public interface IUserService
{
    Task<ResponseDto<UserRegistrationDto>> RegisterUserAsync(UserRegistrationDto userDto);
    Task<ResponseDto<IEnumerable<User>>> GetAllUsersAsync();
}
