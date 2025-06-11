namespace ADAShop.Web.Services.Cart
{
    public interface ICartService
    {
        Task<List<Shared.Entities.Cart>> GetByUserIdAsync(long userId);

        Task<Shared.Entities.Cart> CreateAsync(Shared.Entities.Cart cart);

        Task<Shared.Entities.Cart> GetByIdAsync(int id);

        Task<Shared.Entities.Cart> UpdateAsync(Shared.Entities.Cart cart);
    }
}