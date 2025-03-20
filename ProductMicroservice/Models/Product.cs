using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMicroservice.Models
{
    [Table("Products")]
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string> ImagePaths { get; set; } = new List<string>();

        // Foreign Key for Category
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
