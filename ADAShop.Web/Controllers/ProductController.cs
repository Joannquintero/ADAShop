using ADAShop.Web.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;

        public ProductController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewModels.Product.ProductWithListOfCatesVM product = new();
            product.categories = await _categoryService.GetAllAsync();
            return View(product);
        }
    }
}