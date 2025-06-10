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
            var responseHppt = await _repository.Get<Shared.Entities.User> ($"api/Accounts/Get?userName={userName}");
            return responseHppt.Response!;
        }
    }
}