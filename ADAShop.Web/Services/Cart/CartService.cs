using ADAShop.Web.Repository;

namespace ADAShop.Web.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IRepository _repository;

        public CartService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Shared.Entities.Product>> GetAllAsync()
        {
            var responseHppt = await _repository.Get<List<Shared.Entities.Product>>($"api/Carts/GetAll");
            return responseHppt.Response!;
        }
    }
}