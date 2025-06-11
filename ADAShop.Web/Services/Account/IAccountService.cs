namespace ADAShop.Web.Services.Account
{
    public interface IAccountService
    {
        Task<Shared.Entities.User> GetAsync(string userName);

        Task<Shared.DTOs.TokenDTO> LoginAsync(Shared.DTOs.LoginDTO loginDTO);

        Task<Shared.DTOs.UserDTO> CreateAsync(Shared.DTOs.UserDTO userDTO);

        Task AddToRoleUserAsync(Shared.Entities.User user);
    }
}