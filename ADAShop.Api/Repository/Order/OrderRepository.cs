using ADAShop.Api.Data;
using ADAShop.Api.Repository.Transactions;
using ADAShop.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ADAShop.Api.Repository.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;
        private readonly IOrderItemTransactions _orderItemTransactions;

        public OrderRepository(Context context, IOrderItemTransactions orderItemTransactions)
        {
            _context = context;
            _orderItemTransactions = orderItemTransactions;
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
               .Include(x => x.User)
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
        public async Task<OrderItemDTO> CreateOrderItemAsync(OrderItemDTO orderItemDTO)
        {
            var response = await _orderItemTransactions.Create(orderItemDTO);
            return orderItemDTO;
        }
    }
}