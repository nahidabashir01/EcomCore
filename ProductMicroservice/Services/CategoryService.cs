using AppDbContext.Repository.IRepository;
using ProductMicroservice.Dtos.Category;
using ProductMicroservice.Models;
using ServiceRespnse.Models;
using ServiceRespnse.Repository.IRepository;

namespace ProductMicroservice.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IResponseRepository _responseRepository;

        public CategoryService(IGenericRepository<Category> repository, IResponseRepository responseRepository)
        {
            _repository = repository;
            _responseRepository = responseRepository;
        }
        public async Task<ResponseDto<Guid>> CreateCategoryAsync(CreateCategoryDto categoryCreateDto)
        {
            if (categoryCreateDto == null || string.IsNullOrWhiteSpace(categoryCreateDto.Name))
            {
                return await _responseRepository.FormatResponseAsync<Guid>(
                    false, StatusCodes.Status400BadRequest, "Invalid category data", Guid.Empty);
            }
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryCreateDto.Name
            };

            await _repository.AddAsync(category);

            return await _responseRepository.FormatResponseAsync(
                true, StatusCodes.Status201Created, "Category created successfully", category.Id);
        }

        public async Task<ResponseDto<bool>> DeleteCategoryByIdAsync(Guid categoryId)
        {
            var category = await _repository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return await _responseRepository.FormatResponseAsync<bool>(
                    false, StatusCodes.Status404NotFound, "Category not found", false);
            }

            await _repository.DeleteAsync(category.Id);

            return await _responseRepository.FormatResponseAsync(
                true, StatusCodes.Status200OK, "Category deleted successfully", true);
        }

        public async Task<ResponseDto<bool>> UpdateCategoryAsync(UpdateCategoryDto categoryUpdateDto)
        {
            var category = await _repository.GetByIdAsync(categoryUpdateDto.Id);
            if (category == null)
            {
                return await _responseRepository.FormatResponseAsync<bool>(
                    false, StatusCodes.Status404NotFound, "Category not found", false);
            }

            category.Name = categoryUpdateDto.Name;

            await _repository.UpdateAsync(category);

            return await _responseRepository.FormatResponseAsync(
                true, StatusCodes.Status200OK, "Category updated successfully", true);
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();

            if (categories == null || !categories.Any())
            {
                return await _responseRepository.FormatResponseAsync<List<CategoryDto>>(
                    false, StatusCodes.Status404NotFound, "No categories found", null);
            }

            var categoryDtos = categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            }).ToList();

            return await _responseRepository.FormatResponseAsync(true, StatusCodes.Status200OK, "Categories retrieved successfully", categoryDtos);
        }

    }
}
