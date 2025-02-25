using UserMicroservice.Dtos.Request;
using UserMicroservice.Dtos.Responce;

public interface IUserService
{
    Task<UserCreationResult> RegisterUserAsync(UserRegistrationDto userDto);
    Task<LoginResult> LoginUserAsync(UserLoginDto loginDto);
}
