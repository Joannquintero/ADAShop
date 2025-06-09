using System.ComponentModel.DataAnnotations;

namespace ADAShop.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = null!;

        public ICollection<ProductCategory>? ProductCategories { get; set; }

        public int ProductCategoriesNumber => ProductCategories == null ? 0 : ProductCategories.Count;
    }
}