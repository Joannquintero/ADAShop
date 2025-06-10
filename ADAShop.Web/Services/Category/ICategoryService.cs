namespace ADAShop.Web.Services.Category
{
    public interface ICategoryService
    {
        Task<List<Shared.Entities.Category>> GetAllAsync();
    }
}