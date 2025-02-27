using ServiceRespnse.Models;
using ServiceRespnse.Repository.IRepository;

namespace ServiceRespnse.Repository
{
    public class ResponseRepository : IResponseRepository
    {
        public async Task<ResponseDto<T>> FormatResponseAsync<T>(bool isSuccess, int statusCode, string message, T result)
        {
            return await Task.Run(() => new ResponseDto<T>
            {
                IsSuccess = isSuccess,
                StatusCode = statusCode,
                Message = message,
                Data = result,
            });
        }
    }
}
