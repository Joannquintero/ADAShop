using ADAShop.Shared.DTOs;

namespace ADAShop.Api.Helpers.GenericExecuteSP
{
    public interface IGenericExecuteSP
    {
        Task<T> ExecuteGenericSPAsync<T>(GenericSPExecuteDTO generiRequestDTO);
    }
}
