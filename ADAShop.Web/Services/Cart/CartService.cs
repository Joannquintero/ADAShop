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

        public async Task<List<Shared.Entities.Cart>> GetByUserIdAsync(long userId)
        {
            var responseHppt = await _repository.Get<List<Shared.Entities.Cart>>($"api/Carts/GetByUserId?userId={userId}");
            return responseHppt.Response!;
        }

        public async Task<Shared.Entities.Cart> GetByIdAsync(int id)
        {
            var responseHppt = await _repository.Get<Shared.Entities.Cart>($"api/Carts/GetById?id={id}");
            return responseHppt.Response!;
        }

        public async Task<Shared.Entities.Cart> CreateAsync(Shared.Entities.Cart cart)
        {
            var responseHppt = await _repository.Post<Shared.Entities.Cart, Shared.Entities.Cart>("api/Carts/Insert", cart);
            return responseHppt.Response!;
        }

        public async Task<Shared.Entities.Cart> UpdateAsync(Shared.Entities.Cart cart)
        {
            var responseHppt = await _repository.Post<Shared.Entities.Cart, Shared.Entities.Cart>("api/Carts/Update", cart);
            return responseHppt.Response!;
        }
    }
}