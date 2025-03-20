using MediatR;
using ProductMicroservice.Dtos.Category;
using ProductMicroservice.Dtos.Product;
using ServiceRespnse.Models;

namespace ProductMicroservice.Commands
{
    public class UpdateProductCommand : IRequest<ResponseDto<bool>>
    {
        public UpdateProductDto? UpdateProductDto { get; set; }
    }
}
