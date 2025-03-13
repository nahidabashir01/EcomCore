using MediatR;
using ProductMicroservice.Dtos.Category;
using ServiceRespnse.Models;

namespace ProductMicroservice.Commands
{
    public class CreateCategoryCommand : IRequest<ResponseDto<Guid>>
    {
        public CreateCategoryDto CreateCategoryDto { get; set; }
    }
}
