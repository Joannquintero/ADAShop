using ADAShop.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ADAShop.Api.Repository.Product
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

        public async Task<Shared.Entities.Product> GetByIdAsync(int id)
        {
            var response = await _context.Products
                .Include(x => x.Category)
                .Where(x => x.Id == id)
                 .FirstOrDefaultAsync();
            return response!;
        }

        public async Task<Shared.Entities.Product> CreateAsync(Shared.Entities.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Shared.Entities.Product> UpdateAsync(Shared.Entities.Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}