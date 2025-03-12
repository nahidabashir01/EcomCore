namespace ProductMicroservice.Dtos
{
    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string>? ImagePaths { get; set; }
    }

}
