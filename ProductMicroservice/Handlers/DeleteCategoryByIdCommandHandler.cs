using MediatR;
using ProductMicroservice.Commands;
using ProductMicroservice.Services;
using ServiceRespnse.Models;

namespace ProductMicroservice.Handlers
{
    public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, ResponseDto<bool>>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryByIdCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ResponseDto<bool>> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.DeleteCategoryByIdAsync(request.DeleteCategoryDto.CategoryId);
        }
    }
}
