namespace ADAShop.Api.Repository.Product
{
    public interface IProductRepository
    {
        Task<List<Shared.Entities.Product>> GetAllAsync();

        Task<Shared.Entities.Product> GetByIdAsync(int id);
    }
}