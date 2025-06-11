using ADAShop.Shared.DTOs;

namespace ADAShop.Web.Services.OrderItem
{
    public interface IOrderItemService
    {
        Task<OrderItemDTO> CreateAsync(OrderItemDTO orderItemDTO);
    }
}
