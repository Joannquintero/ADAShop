using ADAShop.Shared.DTOs;
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

        public async Task<OrderItemDTO> CreateAsync(OrderItemDTO orderItemDTO)
        {
            var responseHppt = await _repository.Post<OrderItemDTO, OrderItemDTO>("api/Orders/InsertOrderItem", orderItemDTO);
            return responseHppt.Response!;
        }
    }
}