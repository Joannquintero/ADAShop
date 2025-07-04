using ADAShop.Shared.Emuns;
using ADAShop.Shared.Entities;
using ADAShop.Web.Models;
using ADAShop.Web.Services;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.Product;
using ADAShop.Web.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace ADAShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly IProductService _productService;
        private readonly ITokenService _tokenService;
        private readonly ICartService _cartService;

        public HomeController(
            ILogger<HomeController> logger,
            SignInManager<User> signInManager,
            IProductService productService,
            ITokenService tokenService,
            ICartService cartService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _productService = productService;
            _tokenService = tokenService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Recuperar el token del cookie
            _tokenService.Token = _tokenService.GetTokenCookies(Request)!;
            if (string.IsNullOrEmpty(_tokenService.Token))
            {
                await _signInManager.SignOutAsync();
            }

            var claimsIdentity = HttpContext.Session.Get("ClaimsIdentityModel");
            ClaimsIdentityModel? identitySession = null;
            if (claimsIdentity != null)
            {
                identitySession = JsonSerializer.Deserialize<ClaimsIdentityModel>(claimsIdentity)!;
                ViewBag.ClaimsIdentityViewBag = identitySession;
            }

            List<Product> products = await _productService.GetAllAsync();
            var carts = identitySession != null ? await _cartService.GetByUserIdAsync(identitySession!.UserId) : new List<Cart>();
            carts = carts.Where(x => x.Status == ShoppingCartStatusEnum.NEW.ToString()).ToList();

            string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            ViewBag.UserId = userIdClaim;
            ViewBag.AllProductsNames = products.Select(p => p.Name);

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
                    Products = products,
                    Cart = carts.FirstOrDefault()
                };

                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}