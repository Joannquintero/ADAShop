using ADAShop.Web.ViewModels.Product;

namespace ADAShop.Web.Services.Product
{
    public interface IProductService
    {
        Task<List<Shared.Entities.Product>> GetAllAsync();

        Task<Shared.Entities.Product> GetByIdAsync(int id);

        Task<ProductWithListOfCatesVM> GetViewModel(int id);

        Task<Shared.Entities.Product> CreateAsync(Shared.Entities.Product product);

        Task<Shared.Entities.Product> UpdateAsync(Shared.Entities.Product product);
    }
}