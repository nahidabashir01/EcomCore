using ProductMicroservice.Dtos.Product;
using ServiceRespnse.Models;

namespace ProductMicroservice.Services.IService
{
    public interface IProductService
    {
        Task<ResponseDto<Guid>> CreateProductAsync(CreateProductDto createProductDto);
        Task<ResponseDto<List<ProductDto>>> GetAllProductsAsync();
        Task<ResponseDto<bool>> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<ResponseDto<bool>> DeleteProductAsync(Guid productId);
    }
}
