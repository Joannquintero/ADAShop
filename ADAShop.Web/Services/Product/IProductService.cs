using ADAShop.Api;

namespace ADAShop.Web.Services.Product
{
    public interface IProductService
    {
        Task<List<Shared.Entities.Product>> GetAllAsync();
        Task<List<WeatherForecast>> GetAllTestAsync();
    }
}