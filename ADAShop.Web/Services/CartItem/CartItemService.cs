using ADAShop.Web.Repository;

namespace ADAShop.Web.Services.CartItem
{
    public class CartItemService : ICartItemService
    {
        private readonly IRepository _repository;

        public CartItemService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Entities.CartItem> CreateAsync(Shared.Entities.CartItem cartItem)
        {
            var responseHppt = await _repository.Post<Shared.Entities.CartItem, Shared.Entities.CartItem>("api/Carts/InsertCartItem", cartItem);
            return responseHppt.Response!;
        }

        public async Task<Shared.Entities.CartItem> UpdateAsync(Shared.Entities.CartItem cartItem)
        {
            var responseHppt = await _repository.Put<Shared.Entities.CartItem, Shared.Entities.CartItem>("api/Carts/UpdateCartItem", cartItem);
            return responseHppt.Response!;
        }
    }
}