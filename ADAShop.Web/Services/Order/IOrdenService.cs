namespace ADAShop.Web.Services.Order
{
    public interface IOrdenService
    {
        Task<Shared.Entities.Order> CreateAsync(Shared.Entities.Order order);
    }
}