using ADAShop.Shared.Entities;
using ADAShop.Web.Services.Order;
using ADAShop.Web.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IOrdenService _ordenService;
        private readonly IProductService _productService;

        public DashboardController(IOrdenService ordenService, IProductService productService)
        {
            _ordenService = ordenService;
            _productService = productService;
        }

        [HttpGet]
        //[Authorize("Admin")]
        public async Task<IActionResult> Orders()
        {
            var orders = await _ordenService.GetAllAsync();
            return View(orders);
        }

        [HttpGet]
        [Authorize("Admin")]
        public async Task<IActionResult> Products()
        {
            List<Product> products = await _productService.GetAllAsync();
            return View(products);
        }
    }
}