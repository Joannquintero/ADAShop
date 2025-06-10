using System.ComponentModel.DataAnnotations;

namespace ADAShop.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Product>? Products { get; set; }
    }
}