using System.ComponentModel.DataAnnotations.Schema;

namespace ADAShop.Shared.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        public decimal Quantity { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public decimal Stock { get; set; }

        public int CartId { get; set; }
    }
}