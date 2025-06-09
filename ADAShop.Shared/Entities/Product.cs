using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADAShop.Shared.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sin descripción")]
        [MaxLength(500)]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public float Stock { get; set; }

        public string? Image { get; set; } = "wwwroot/images/not_available.png";  // default image

        public int ProductCategoriesNumber => ProductCategories == null ? 0 : ProductCategories.Count;

        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}