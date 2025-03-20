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
    }
}