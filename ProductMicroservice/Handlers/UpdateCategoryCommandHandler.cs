using MediatR;
using ProductMicroservice.Commands;
using ProductMicroservice.Services;
using ServiceRespnse.Models;

namespace ProductMicroservice.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ResponseDto<bool>>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ResponseDto<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.UpdateCategoryAsync(request.UpdateCategoryDto);
        }
    }
}
