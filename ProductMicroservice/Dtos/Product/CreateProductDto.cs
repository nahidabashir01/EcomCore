namespace ProductMicroservice.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

}
