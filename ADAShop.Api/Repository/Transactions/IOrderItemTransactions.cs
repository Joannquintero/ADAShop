using ADAShop.Shared.DTOs;

namespace ADAShop.Api.Repository.Transactions
{
    public interface IOrderItemTransactions
    {
        Task<GenericResultDTO> Create(OrderItemDTO orderItemDTO);
    }
}