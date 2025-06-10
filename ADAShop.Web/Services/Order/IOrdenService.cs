namespace ADAShop.Web.Services.Order
{
    public interface IOrdenService
    {
        Task<List<Shared.Entities.Order>> GetAllAsync();

        Task<Shared.Entities.Order> CreateAsync(Shared.Entities.Order order);
    }
}