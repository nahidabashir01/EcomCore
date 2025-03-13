using ProductMicroservice.Dtos.Category;
using ServiceRespnse.Models;

namespace ProductMicroservice.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<Guid>> CreateCategoryAsync(CreateCategoryDto dto);
        Task<ResponseDto<bool>> UpdateCategoryAsync(UpdateCategoryDto dto);
        Task<ResponseDto<bool>> DeleteCategoryByIdAsync(Guid categoryId);

        Task<ResponseDto<List<CategoryDto>>> GetAllCategoriesAsync();
    }
}
