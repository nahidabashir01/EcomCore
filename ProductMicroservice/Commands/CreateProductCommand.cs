using MediatR;
using ProductMicroservice.Dtos;
using ServiceRespnse.Models;

namespace ProductMicroservice.Commands
{
    public class CreateProductCommand:IRequest<ResponseDto<Guid>>
    {
        public ProductCreateDto? ProductCreateDto {  get; set; }
    }
}
