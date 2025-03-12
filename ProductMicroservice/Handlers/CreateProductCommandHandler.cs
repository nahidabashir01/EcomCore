using MediatR;
using ProductMicroservice.Commands;
using ProductMicroservice.Services.IService;
using ServiceRespnse.Models;

namespace ProductMicroservice.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseDto<Guid>>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ResponseDto<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.CreateProductAsync(request.ProductCreateDto);
        }
    }
}
