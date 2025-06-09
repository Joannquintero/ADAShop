using System.ComponentModel.DataAnnotations.Schema;

namespace ADAShop.Shared.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public long? UserId { get; set; }

        public User? User { get; set; }

        public List<CartItem>? CartItems { get; set; }
    }
}