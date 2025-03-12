using ProductMicroservice.Dtos;
using ServiceRespnse.Models;

namespace ProductMicroservice.Services.IService
{
    public interface IProductService
    {
        Task<ResponseDto<Guid>> CreateProductAsync(ProductCreateDto productCreateDto);
    }
}
