using Microsoft.AspNetCore.Identity.Data;
using ServiceRespnse.Models;
using UserMicroservice.Dtos.Request;

public interface IUserService
{
    Task<ResponseDto<UserRegistrationDto>> RegisterUserAsync(UserRegistrationDto userDto);
}
