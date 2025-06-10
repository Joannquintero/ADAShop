namespace ADAShop.Web.Services.Cart
{
    public interface ICartService
    {
        Task<List<Shared.Entities.Cart>> GetByUserIdAsync(long userId);

        Task<Shared.Entities.Cart> CreateAsync(Shared.Entities.Cart cart); 
    }
}