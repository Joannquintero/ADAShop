namespace ADAShop.Web.Services.CartItem
{
    public interface ICartItemService
    {
        Task<Shared.Entities.CartItem> CreateAsync(Shared.Entities.CartItem cartItem);
    }
}