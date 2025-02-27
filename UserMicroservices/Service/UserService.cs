using AppDbContext.Repository.IRepository;
using Constants;
using InMemoryCache.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using ServiceRespnse.Models;
using ServiceRespnse.Repository.IRepository;
using ServiceRespnse.Utility;
using System.Net;
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

        var databaseResponse = await _repository.AddAsync(newUser);
        if (databaseResponse)
        {
            var setCacheResponse =await SetData(CacheKeys.User);
            return await _responseRepository.FormatResponseAsync(true, StatusCode.Created, ResponseMessages.CreatedMessage, userDto);
        }
        return await _responseRepository.FormatResponseAsync(false, StatusCode.BadRequest, ResponseMessages.BadRequestMessage, userDto);
    }

    public async Task<ResponseDto<IEnumerable<User>>> ReadAllUserAsync()
    {
        var userList = GetData(CacheKeys.User);

        if (userList.IsNullOrEmpty())
        {
            var setCacheResponse = await SetData(CacheKeys.User);
            userList = setCacheResponse.Item2;
            if (setCacheResponse.Item1)
            {

            }
        }

        return await _responseRepository.FormatResponseAsync(true, (int)HttpStatusCode.OK, "Success", userList);
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

    private async Task<(bool,IEnumerable<User>)> SetData(string key)
    {
        var data = await _repository.GetAllAsync();
        var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
        var success = _cacheRepository.SetData(key, data, expirationTime);
        return (success,data);
    }
}
