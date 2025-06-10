namespace ADAShop.Api.Repository.Order
{
    public interface IOrderRepository
    {
        Task<Shared.Entities.Order> CreateAsync(Shared.Entities.Order order);

        Task<List<Shared.Entities.Order>> GetAllAsync();

        Task<Shared.Entities.OrderItem> CreateOrderItemAsync(Shared.Entities.OrderItem orderItem);
    }
}