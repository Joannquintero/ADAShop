using ADAShop.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ADAShop.Api.Repository.Cart
{
    public class CartRepository : ICartRepository
    {
        private readonly Context _context;

        public CartRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Shared.Entities.Cart>> GetAllAsync()
        {
            var queryable = _context.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                 .AsQueryable();

            return await queryable
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<Shared.Entities.Cart>> GetByUserIdAsync(long userId)
        {
            var response = _context.Carts
                .Include(x => x.CartItems!)
                .ThenInclude(x => x.Product)
                .Where(x => x.UserId == userId)
                 .AsQueryable();

            return await response
                     .OrderBy(x => x.Id)
                     .ToListAsync();
        }

        public async Task<Shared.Entities.Cart> CreateAsync(Shared.Entities.Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Shared.Entities.CartItem> CreateCartItemAsync(Shared.Entities.CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
    }
}