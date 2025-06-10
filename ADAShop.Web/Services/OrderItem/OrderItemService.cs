using ADAShop.Web.Repository;

namespace ADAShop.Web.Services.OrderItem
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository _repository;

        public OrderItemService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Entities.OrderItem> CreateAsync(Shared.Entities.OrderItem orderItem)
        {
            var responseHppt = await _repository.Post<Shared.Entities.OrderItem, Shared.Entities.OrderItem>("api/Orders/InsertOrderItem", orderItem);
            return responseHppt.Response!;
        }
    }
}