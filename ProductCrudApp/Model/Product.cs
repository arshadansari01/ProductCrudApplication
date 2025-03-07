using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCrudApp.Model
{
    public class Product
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("Category")]  // Explicitly marks it as a Foreign Key
        public int CategoryId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow; // Auto-set when created

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow; // Auto-updated on modification

    }
}

