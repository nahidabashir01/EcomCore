using ServiceRespnse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRespnse.Repository.IRepository
{
    public interface IResponseRepository
    {
        Task<ResponseDto<T>> FormatResponseAsync<T>(bool isSuccess, int statusCode, string message, T result);
    }
}
