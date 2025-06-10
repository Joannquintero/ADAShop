using ADAShop.Web.Repository;

namespace ADAShop.Web.Services.Order
{
    public class OrdenService : IOrdenService
    {
        private readonly IRepository _repository;

        public OrdenService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Entities.Order> CreateAsync(Shared.Entities.Order order)
        {
            var responseHppt = await _repository.Post<Shared.Entities.Order, Shared.Entities.Order>("api/Orders/Insert", order);
            return responseHppt.Response!;
        }
    }
}