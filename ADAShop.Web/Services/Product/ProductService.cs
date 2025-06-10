using ADAShop.Api;
using ADAShop.Web.Repository;

namespace ADAShop.Web.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Shared.Entities.Product>> GetAllAsync()
        {
            var responseHppt = await _repository.Get<List<Shared.Entities.Product>>($"api/Products/GetAll");
            return responseHppt.Response!;
        }

        public async Task<Shared.Entities.Product> GetByIdAsync(int id)
        {
            var responseHppt = await _repository.Get<Shared.Entities.Product>($"api/Products/GetById?id={id}");
            return responseHppt.Response!;
        }

        //TODO: [JAN] - Summary
        public async Task<List<WeatherForecast>> GetAllTestAsync()
        {
            var responseHppt = await _repository.Get<List<WeatherForecast>>("WeatherForecast");
            return responseHppt.Response!;
        }
    }
}