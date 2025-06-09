using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADAShop.Shared.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public float Quantity { get; set; }

        //[ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        //[ForeignKey("Cart")]
        public int? CartId { get; set; }

        public Cart? Cart { get; set; }
    }
}