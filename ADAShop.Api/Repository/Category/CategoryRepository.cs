using ADAShop.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ADAShop.Api.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Shared.Entities.Category>> GetAllAsync()
        {
            var queryable = _context.Categories
                 .AsQueryable();
            return await queryable
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}