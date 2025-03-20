using MediatR;
using ProductMicroservice.Dtos.Product;
using ProductMicroservice.Queries;
using ProductMicroservice.Services;
using ServiceRespnse.Models;
using ServiceRespnse.Repository.IRepository;

namespace ProductMicroservice.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ResponseDto<List<ProductDto>>>
    {
        private readonly ProductService _productService;

        public GetAllProductsHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<ResponseDto<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProductsAsync();
            return products;
        }
    }
}
