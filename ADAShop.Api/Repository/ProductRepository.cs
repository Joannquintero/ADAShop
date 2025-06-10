using ADAShop.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ADAShop.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Shared.Entities.Product>> GetAllAsync()
        {
            var queryable = _context.Products
                .Include(x => x.Category)
                 .AsQueryable();
            return await queryable
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}
