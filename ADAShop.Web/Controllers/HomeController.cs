using ADAShop.Shared.Entities;
using ADAShop.Web.Models;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.Product;
using ADAShop.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ADAShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService,
            ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productService.GetAllAsync();
            //List<Cart> carts = _cartService.GetAll();
            List<Cart> carts = new List<Cart>();

            string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            ViewBag.UserId = userIdClaim;
            //ViewBag.AllProductsNames = productService.GetAll().Select(p => p.Name);

            if (carts.Count == 0)
            {
                Cart cart = new Cart() { CartItems = new List<CartItem>() };

                ProdCatCartVM model = new ProdCatCartVM()
                {
                    Products = products,
                    Cart = cart,
                };

                return View(model);
            }
            else
            {
                ProdCatCartVM model = new ProdCatCartVM()
                {
                    //Categories = categoryService.GetAll(),
                    //Products = products,
                    //Cart = cartService.GetAll("CartItems").FirstOrDefault(),
                };

                return View(model);
            }
        }

        //public async Task<IActionResult> Search(string searchProdName)
        //{
        //    List<Product> searchedProducts = await _productService.GetAllAsync();
        //    searchedProducts = searchedProducts.Where(p => p.Name.Contains(searchProdName)).ToList();
        //    int id = searchedProducts.Select(p => p.Id).FirstOrDefault();
        //    return RedirectToAction("Details", "Product", new { id });
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}