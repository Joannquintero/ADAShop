namespace ADAShop.Web.Services.OrderItem
{
    public interface IOrderItemService
    {
        Task<Shared.Entities.OrderItem> CreateAsync(Shared.Entities.OrderItem orderItem);
    }
}
