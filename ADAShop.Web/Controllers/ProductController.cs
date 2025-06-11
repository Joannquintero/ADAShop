using ADAShop.Shared.Entities;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ICategoryService categoryService, IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _categoryService = categoryService;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Update(int id)
        {
            ProductWithListOfCatesVM product = await _productService.GetViewModel(id);
            if (product != null)
            {
                return View(product);
            }

            return RedirectToAction("products", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductWithListOfCatesVM model)
        {
            string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            string? imagename = model.image != null ? Guid.NewGuid().ToString() + "_" + model.image!.FileName : null;
            if (model.image != null)
            {
                string filepath = Path.Combine(uploadpath, imagename!);
                using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    model.image!.CopyTo(fileStream);
                }
            }

            model.ImageUrl = imagename;
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Image = imagename,
                    Price = model.Price,
                    Stock = model.Quantity,
                    CategoryId = model.CategoryId
                };
                var response = await _productService.CreateAsync(product);
                return RedirectToAction("Products", "Dashboard");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductWithListOfCatesVM model)
        {
            string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            string? imagename = model.image != null ? Guid.NewGuid().ToString() + "_" + model.image!.FileName : null;
            if (model.image != null)
            {
                string filepath = Path.Combine(uploadpath, imagename!);
                using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    model.image!.CopyTo(fileStream);
                }
            }

            model.ImageUrl = imagename;

            if (ModelState.IsValid)
            {
                var product = await _productService.GetByIdAsync(model.Id);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Image = model.image != null ? model.ImageUrl : product.Image;
                product.Price = model.Price;
                product.Stock = model.Quantity;
                product.CategoryId = model.CategoryId;
                product.Category = null;
                if (product != null)
                {
                    var response = await _productService.UpdateAsync(product);
                    return RedirectToAction("Products", "Dashboard");
                }
            }
            return View(model);
        }
    }
}