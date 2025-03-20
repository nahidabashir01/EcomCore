using MediatR;
using ProductMicroservice.Commands;
using ProductMicroservice.Services.IService;
using ServiceRespnse.Models;

namespace ProductMicroservice.Handlers
{
    public class UpdateProductHandler: IRequestHandler<UpdateProductCommand, ResponseDto<bool>>
    {
        private readonly IProductService _productService;

        public UpdateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ResponseDto<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProductAsync(request.UpdateProductDto);
        }
    }
}
