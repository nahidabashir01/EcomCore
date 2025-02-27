using AppDbContext.Repository.IRepository;
using Microsoft.AspNetCore.Identity.Data;
using ServiceRespnse.Models;
using ServiceRespnse.Repository.IRepository;
using ServiceRespnse.Utility;
using UserMicroservice.Dtos.Request;
using UserMicroservice.Models;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _repository;
    private readonly IResponseRepository _responseRepository;

    public UserService(IGenericRepository<User> repository, IResponseRepository responseRepository)
    {
        _repository = repository;
        _responseRepository = responseRepository;
    }

    public async Task<ResponseDto<UserRegistrationDto>> RegisterUserAsync(UserRegistrationDto userDto)
    {
        var existingUser = await _repository.FindAsync(x => x.Email == userDto.Email);
        if (existingUser.Any())
        {
            return await _responseRepository.FormatResponseAsync<UserRegistrationDto>(false, StatusCode.Conflict, ResponseMessages.ConflictMessage, null);
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

        await _repository.AddAsync(newUser);
        return await _responseRepository.FormatResponseAsync<UserRegistrationDto>(true, StatusCode.Created, ResponseMessages.CreatedMessage, userDto);
    }

    public async Task<ResponseDto<LoginRequest>> LoginUserAsync(UserLoginDto loginDto)
    {
        var allUsersByEmail = await _repository.FindAsync(x => x.Email == loginDto.Email);
        var user = allUsersByEmail.FirstOrDefault();
        if (user is not null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            return await _responseRepository.FormatResponseAsync<LoginRequest>(false, StatusCode.Unauthorized, ResponseMessages.UnauthrizedMessage, null);
        }
        return await _responseRepository.FormatResponseAsync<LoginRequest>(true, StatusCode.OK, ResponseMessages.OkMessage, null);
    }
}
