using MediatR;
using ProductMicroservice.Dtos.Category;
using ProductMicroservice.Queries;
using ProductMicroservice.Services;
using ServiceRespnse.Models;

namespace ProductMicroservice.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, ResponseDto<List<CategoryDto>>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoriesHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ResponseDto<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllCategoriesAsync();
        }
    }

}
