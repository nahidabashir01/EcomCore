using UserMicroservice.Dtos.Request;
using UserMicroservice.Dtos.Responce;
using UserMicroservice.Models;
using UserMicroservice.Repositories;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserCreationResult> RegisterUserAsync(UserRegistrationDto userDto)
    {
        var existingUser = await _repository.GetUserByEmailAsync(userDto.Email);
        if (existingUser != null)
        {
            return new UserCreationResult
            {
                Success = false,
                Message = "User already exists with this email."
            };
        }

        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            Name = userDto.Name,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Role = "User"
        };

        await _repository.AddUserAsync(newUser);
        return new UserCreationResult
        {
            Success = true,
            Message = "User successfully created."
        };
    }

    public async Task<LoginResult> LoginUserAsync(UserLoginDto loginDto)
    {
        var user = await _repository.GetUserByEmailAsync(loginDto.Email);
        if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            return new LoginResult
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }
        return new LoginResult
        {
            Success = true,
            Message = "Login successful."
        };
    }
}
