using MediatR;
using ProductMicroservice.Dtos.Category;
using ServiceRespnse.Models;

namespace ProductMicroservice.Commands
{
    public class DeleteCategoryByIdCommand : IRequest<ResponseDto<bool>>
    {
        public DeleteCategoryDto DeleteCategoryDto { get; set; }
    }
}
