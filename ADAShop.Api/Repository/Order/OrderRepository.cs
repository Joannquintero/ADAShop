using ADAShop.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ADAShop.Api.Repository.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
        }

        public async Task<Shared.Entities.Order> CreateAsync(Shared.Entities.Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Shared.Entities.Order>> GetAllAsync()
        {
            var queryable = _context.Orders
               .Include(x => x.OrderItems)
                .AsQueryable();

            return await queryable
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        /// <summary>
        /// Se ejecuta por medio de SP segun lo solicitado en la prueba para controlar transacciones
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public async Task<Shared.Entities.OrderItem> CreateOrderItemAsync(Shared.Entities.OrderItem orderItem)
        {
            return null;
        }
    }
}