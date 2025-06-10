using ADAShop.Web.Services.Order;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IOrdenService _ordenService;

        public DashboardController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        public async Task<IActionResult> Orders()
        {
            var orders = await _ordenService.GetAllAsync();
            return View(orders);
        }
    }
}