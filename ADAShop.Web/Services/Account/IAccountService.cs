namespace ADAShop.Web.Services.Account
{
    public interface IAccountService
    {
        Task<Shared.Entities.User> GetAsync(string userName);

        Task<Shared.DTOs.TokenDTO> LoginAsync(Shared.DTOs.LoginDTO loginDTO);
    }
}