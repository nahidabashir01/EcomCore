using AppDbContext.Repository.IRepository;
using Constants;
using InMemoryCache.Repository.IRepository;
using ServiceRespnse.Models;
using ServiceRespnse.Repository.IRepository;
using ServiceRespnse.Utility;
using UserMicroservice.Dtos.Request;
using UserMicroservice.Models;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _repository;
    private readonly IResponseRepository _responseRepository;
    private readonly ICacheRepository _cacheRepository;

    public UserService(IGenericRepository<User> repository, IResponseRepository responseRepository, ICacheRepository cacheRepository)
    {
        _repository = repository;
        _responseRepository = responseRepository;
        _cacheRepository = cacheRepository;
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

        var result = await _repository.AddAsync(newUser);
        if (result)
        {
            var isSuccess = SetData(CacheKeys.User);
            return await _responseRepository.FormatResponseAsync(true, StatusCode.Created, ResponseMessages.CreatedMessage, userDto);
        }
        return await _responseRepository.FormatResponseAsync(false, StatusCode.BadRequest, ResponseMessages.BadRequestMessage, userDto);
    }

    private IEnumerable<User> GetData(string key)
    {
        var cacheData = _cacheRepository.GetData<IEnumerable<User>>(key);
        if (cacheData != null)
        {
            return cacheData;
        }
        return null;
    }

    private async Task<bool> SetData(string key)
    {
        var data = await _repository.GetAllAsync();
        var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
        var success = _cacheRepository.SetData(key, data, expirationTime);
        return success;
    }
}
