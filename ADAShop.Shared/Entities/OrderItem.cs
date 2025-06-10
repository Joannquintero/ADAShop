using System.ComponentModel.DataAnnotations.Schema;

namespace ADAShop.Shared.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public decimal Quantity { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; } = null!;
    }
}