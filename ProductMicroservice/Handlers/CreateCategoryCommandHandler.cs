using MediatR;
using ProductMicroservice.Commands;
using ProductMicroservice.Services;
using ServiceRespnse.Models;

namespace ProductMicroservice.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResponseDto<Guid>>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ResponseDto<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.CreateCategoryAsync(request.CreateCategoryDto);
        }
    }
}
