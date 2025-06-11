using ADAShop.Shared.DTOs;
using ADAShop.Web.Repository;

namespace ADAShop.Web.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _repository;

        public AccountService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Entities.User> GetAsync(string userName)
        {
            var responseHppt = await _repository.Get<Shared.Entities.User>($"api/Accounts/Get?userName={userName}");
            return responseHppt.Response!;
        }

        public async Task<Shared.DTOs.TokenDTO> LoginAsync(Shared.DTOs.LoginDTO loginDTO)
        {
            var responseHppt = await _repository.Post<Shared.DTOs.LoginDTO, Shared.DTOs.TokenDTO>("api/Accounts/Login", loginDTO);
            return responseHppt.Response!;
        }

        public async Task<Shared.DTOs.UserDTO> CreateAsync(Shared.DTOs.UserDTO userDTO)
        {
            var responseHppt = await _repository.Post<Shared.DTOs.UserDTO, Shared.DTOs.UserDTO>("api/Accounts/CreateUser", userDTO);
            return responseHppt.Response!;
        }

        public async Task AddToRoleUserAsync(UserRoleDTO userRoleDTO)
        {
            var responseHppt = await _repository.Post("api/Accounts/AddUserToRole", userRoleDTO);
        }

        public async Task<bool> IsUserInRoleAsync(UserRoleDTO userRoleDTO)
        {
            var responseHppt = await _repository.Post<UserRoleDTO, bool>("api/Accounts/IsUserInRole", userRoleDTO);
            return responseHppt.Response;
        }
    }
}