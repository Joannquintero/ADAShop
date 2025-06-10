namespace ADAShop.Api.Repository.Product
{
    public interface IProductRepository
    {
        Task<List<Shared.Entities.Product>> GetAllAsync();
    }
}
