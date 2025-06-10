namespace ADAShop.Api.Repository.Cart
{
    public interface ICartRepository
    {
        Task<List<Shared.Entities.Cart>> GetAllAsync();

        Task<List<Shared.Entities.Cart>> GetByUserIdAsync(long userId);

        Task<Shared.Entities.Cart> CreateAsync(Shared.Entities.Cart cart);

        Task<Shared.Entities.CartItem> CreateCartItemAsync(Shared.Entities.CartItem cartItem);
    }
}