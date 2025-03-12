using AppDbContext.Repository.IRepository;
using ProductMicroservice.Dtos;
using ProductMicroservice.Models;
using ProductMicroservice.Services.IService;
using ServiceRespnse.Models;
using ServiceRespnse.Repository.IRepository;


namespace ProductMicroservice.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IWebHostEnvironment _environment;
        private readonly IResponseRepository _responseRepository;

        public ProductService(IGenericRepository<Product> repository, IWebHostEnvironment environment, IResponseRepository responseRepository)
        {
            _repository = repository;
            _environment = environment;
            _responseRepository = responseRepository;
        }

        public async Task<ResponseDto<Guid>> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null)
            {
                return await _responseRepository.FormatResponseAsync<Guid>(false, StatusCodes.Status400BadRequest, "Invalid product data", Guid.Empty);
            }

            var product = new Product
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                ImagePaths = new List<string>()
            };

            if (productCreateDto.Images != null && productCreateDto.Images.Any())
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                foreach (var image in productCreateDto.Images)
                {
                    if (image.Length > 0)
                    {
                        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        product.ImagePaths.Add($"/uploads/{uniqueFileName}");
                    }
                }
            }

            await _repository.AddAsync(product);

            return await _responseRepository.FormatResponseAsync(true, StatusCodes.Status201Created, "Product created successfully", product.Id);

        }
    }
}