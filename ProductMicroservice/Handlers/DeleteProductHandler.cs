using MediatR;
using ProductMicroservice.Commands;
using ProductMicroservice.Services.IService;
using ServiceRespnse.Models;

namespace ProductMicroservice.Handlers
{
    public class DeleteProductHandler :IRequestHandler<DeleteProductCommand, ResponseDto<bool>>
    {
        private readonly IProductService _productService;

        public DeleteProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ResponseDto<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProductAsync(request.ProductId);
        }
    }
}
