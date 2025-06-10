using ADAShop.Web.Services.Category;
using ADAShop.Web.Services.Product;
using ADAShop.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewModels.Product.ProductWithListOfCatesVM product = new();
            product.categories = await _categoryService.GetAllAsync();
            return View(product);
        }

        //TODO: [JAN] - Summary
        [HttpGet]
        public async Task<IActionResult> Update(int id = 1)
        {
            ProductWithListOfCatesVM product = await _productService.GetViewModel(id);
            if (product != null)
            {
                return View(product);
            }

            return RedirectToAction("products", "Dashboard");
        }
    }
}