namespace ADAShop.Web.Services.Account
{
    public interface IAccountService
    {
        Task<Shared.Entities.User> GetAsync(string userName);
    }
}