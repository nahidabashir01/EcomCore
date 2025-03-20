using AppDbContext.Repository.IRepository;
using ProductMicroservice.Dtos.Product;
using ProductMicroservice.Models;
using ProductMicroservice.Services.IService;
using ServiceRespnse.Models;
using ServiceRespnse.Repository.IRepository;


namespace ProductMicroservice.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly FileStorageService _fileStorageService;
        private readonly IResponseRepository _responseRepository;

        public ProductService(IGenericRepository<Product> repository, FileStorageService fileStorageService, IResponseRepository responseRepository)
        {
            _repository = repository;
            _responseRepository = responseRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<ResponseDto<Guid>> CreateProductAsync(CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return await _responseRepository.FormatResponseAsync<Guid>(false, StatusCodes.Status400BadRequest, "Invalid product data", Guid.Empty);
            }

            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                CategoryId= createProductDto.CategoryId,
                ImagePaths = new List<string>()
            };

            if (createProductDto.Images != null && createProductDto.Images.Any())
            {
                product.ImagePaths = await _fileStorageService.SaveFilesAsync(createProductDto.Images);
            }

            await _repository.AddAsync(product);
            return await _responseRepository.FormatResponseAsync(true, StatusCodes.Status201Created, "Product created successfully", product.Id);

        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();

            var productDtos= products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                Images = _fileStorageService.GetFilesAsByteArray(p.ImagePaths)
            }).ToList();

            return await _responseRepository.FormatResponseAsync(true, 200, "Products retrieved successfully", productDtos);

        }

        public async Task<ResponseDto<bool>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = await _repository.GetByIdAsync(updateProductDto.Id);
            if (product == null)
                return await _responseRepository.FormatResponseAsync(false, 404, "Product not found", false);

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.CategoryId = updateProductDto.CategoryId;

            if (updateProductDto.Images != null && updateProductDto.Images.Count > 0)
            {
                bool allDeleted = true;
                foreach (var imagePath in product.ImagePaths)
                {
                    bool deleted = await _fileStorageService.DeleteFileAsync(imagePath);
                    if (!deleted)
                    {
                        allDeleted = false;
                    }
                }

                if (!allDeleted)
                {
                    return await _responseRepository.FormatResponseAsync(false, 500, "Failed to delete existing images", false);
                }

                product.ImagePaths = await _fileStorageService.SaveFilesAsync(updateProductDto.Images);
            }

            await _repository.UpdateAsync(product);
            return await _responseRepository.FormatResponseAsync(true, 200, "Product updated successfully", true);
        }


        public async Task<ResponseDto<bool>> DeleteProductAsync(Guid productId)
        {
            var product = await _repository.GetByIdAsync(productId);
            if (product == null)
                return await _responseRepository.FormatResponseAsync(false, 404, "Product not found", false);

            bool allImagesDeleted = true;
            foreach (var imageName in product.ImagePaths)
            {
                var result = await _fileStorageService.DeleteFileAsync(imageName);
                if (!result)
                {
                    allImagesDeleted = false;
                }
            }

            var deleted = await _repository.DeleteAsync(productId);
            if (!deleted)
            {
                return await _responseRepository.FormatResponseAsync(false, 500, "Error deleting product", false);
            }

            var message = allImagesDeleted ? "Product and images deleted successfully"
                                           : "Product deleted, but some images could not be removed";

            return await _responseRepository.FormatResponseAsync(true, 200, message, true);
        }

    }
}