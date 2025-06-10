namespace ADAShop.Web.Services.Product
{
    public interface IProductService
    {
        Task<List<Shared.Entities.Product>> GetAllAsync();

        Task<Shared.Entities.Product> GetByIdAsync(int id);
    }
}