using ADAShop.Web.Repository;
using ADAShop.Web.ViewModels.Product;

namespace ADAShop.Web.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly string _version = "v1";

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Shared.Entities.Product>> GetAllAsync()
        {
            var responseHppt = await _repository.Get<List<Shared.Entities.Product>>($"api/{_version}/Products/GetAll");
            return responseHppt.Response!;
        }

        public async Task<Shared.Entities.Product> GetByIdAsync(int id)
        {
            var responseHppt = await _repository.Get<Shared.Entities.Product>($"api/{_version}/Products/{_version}/GetById?id={id}");
            return responseHppt.Response!;
        }

        public async Task<ProductWithListOfCatesVM> GetViewModel(int id)
        {
            var response = await _repository.Get<Shared.Entities.Product>($"api/{_version}/Products/{_version}/GetById?id={id}");
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

            var responseCategories = await _repository.Get<List<Shared.Entities.Category>>($"api/{_version}/Categories/GetAll");
            model.categories = responseCategories.Response;
            return model;
        }

        public async Task<Shared.Entities.Product> CreateAsync(Shared.Entities.Product product)
        {
            var responseHppt = await _repository.Post<Shared.Entities.Product, Shared.Entities.Product>($"api/{_version}/Products/Insert", product);
            return responseHppt.Response!;
        }

        public async Task<Shared.Entities.Product> UpdateAsync(Shared.Entities.Product product)
        {
            var responseHppt = await _repository.Post<Shared.Entities.Product, Shared.Entities.Product>($"api/{_version}//Products/Update", product);
            return responseHppt.Response!;
        }
    }
}