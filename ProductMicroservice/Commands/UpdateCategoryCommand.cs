using MediatR;
using ProductMicroservice.Dtos.Category;
using ServiceRespnse.Models;

namespace ProductMicroservice.Commands
{
    public class UpdateCategoryCommand : IRequest<ResponseDto<bool>>
    {
        public UpdateCategoryDto UpdateCategoryDto { get; set; }

    }
}
