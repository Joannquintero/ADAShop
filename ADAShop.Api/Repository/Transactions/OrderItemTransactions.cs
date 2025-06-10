using ADAShop.Api.Helpers.GenericExecuteSP;
using ADAShop.Shared.DTOs;
using ADAShop.Shared.Emuns;

namespace ADAShop.Api.Repository.Transactions
{
    public class OrderItemTransactions : IOrderItemTransactions
    {
        private readonly IGenericExecuteSP _genericExecuteSP;

        public OrderItemTransactions(IGenericExecuteSP genericExecuteSP)
        {
            _genericExecuteSP = genericExecuteSP;
        }

        public async Task<GenericResultDTO> Create(Shared.Entities.OrderItem orderItem)
        {
            List<ParamGeneric> paramGenerics = new() {
                new ParamGeneric() { DataType = "bigint", Key = "@ProductId", Value = orderItem.ProductId.ToString() },
                new ParamGeneric() { DataType = "bigint", Key = "@OrderId", Value = orderItem.OrderId.ToString() },
                new ParamGeneric() { DataType = "string", Key = "@Quantity", Value = orderItem.Quantity.ToString()  },
            };

            GenericSPExecuteDTO genericSPExecuteDTO = new()
            {
                NameStoreProcedure = "OrderItem_Create",
                NameDataBaseSelection = "DefaultConnection",
                Params = paramGenerics,
                ActionSP = ActionSPEnum.Insert
            };
            GenericResultDTO genericResultDTO = await _genericExecuteSP.ExecuteGenericSPAsync<GenericResultDTO>(genericSPExecuteDTO);
            return genericResultDTO;
        }
    }
}