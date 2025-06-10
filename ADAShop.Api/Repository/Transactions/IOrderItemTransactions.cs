using ADAShop.Shared.DTOs;

namespace ADAShop.Api.Repository.Transactions
{
    public interface IOrderItemTransactions
    {
        Task<GenericResultDTO> Create(Shared.Entities.OrderItem orderItem);
    }
}