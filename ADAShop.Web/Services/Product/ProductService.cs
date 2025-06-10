using ADAShop.Web.Repository;
using ADAShop.Web.ViewModels.Product;

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

        public async Task<ProductWithListOfCatesVM> GetViewModel(int id)
        {
            var response = await _repository.Get<Shared.Entities.Product>($"api/Products/GetById?id={id}");
            ProductWithListOfCatesVM model = new()
            {
                Id = response.Response!.Id,
                Name = response.Response.Name,
                Description = response.Response.Description,
                ImageUrl = response.Response.Image!,
                Price = response.Response.Price,
                Quantity = response.Response.Stock,
                CategoryId = response.Response.Category != null ? response.Response.Category!.Id : 0
            };

            var responseCategories = await _repository.Get<List<Shared.Entities.Category>>($"api/Categories/GetAll");
            model.categories = responseCategories.Response;
            return model;
        }
    }
}