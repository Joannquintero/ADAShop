namespace ADAShop.Api.Repository
{
    public interface IProductRepository
    {
        Task<List<Shared.Entities.Product>> GetAllAsync();
    }
}
