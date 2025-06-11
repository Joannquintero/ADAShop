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

        public async Task<GenericResultDTO> Create(OrderItemDTO orderItemDTO)
        {
            List<ParamGeneric> paramGenerics = new() {
                new ParamGeneric() { DataType = "bigint", Key = "@ProductId", Value = orderItemDTO.ProductId.ToString() },
                new ParamGeneric() { DataType = "bigint", Key = "@OrderId", Value = orderItemDTO.OrderId.ToString() },
                new ParamGeneric() { DataType = "string", Key = "@Quantity", Value = orderItemDTO.Quantity.ToString()  },
                new ParamGeneric() { DataType = "string", Key = "@Stock", Value = orderItemDTO.Stock.ToString()  },
                new ParamGeneric() { DataType = "string", Key = "@CartId", Value = orderItemDTO.CartId.ToString() },
                new ParamGeneric() { DataType = "string", Key = "@Amount", Value = orderItemDTO.Amount.ToString() },
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