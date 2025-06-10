namespace ADAShop.Api.Repository.Category
{
    public interface ICategoryRepository
    {
        Task<List<Shared.Entities.Category>> GetAllAsync();
    }
}