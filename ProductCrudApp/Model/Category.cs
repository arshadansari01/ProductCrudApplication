using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCrudApp.Model
{
    public class Category
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        // Initialize as an empty list to avoid null reference issues
        public List<Product>? Products { get; set; } = new List<Product>();
    }
}
