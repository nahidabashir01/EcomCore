namespace ProductMicroservice.Dtos
{
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

}
