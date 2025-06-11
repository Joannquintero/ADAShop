using ADAShop.Shared.DTOs;

namespace ADAShop.Api.Repository.Order
{
    public interface IOrderRepository
    {
        Task<Shared.Entities.Order> CreateAsync(Shared.Entities.Order order);

        Task<List<Shared.Entities.Order>> GetAllAsync();

        Task<OrderItemDTO> CreateOrderItemAsync(OrderItemDTO orderItem);
    }
}