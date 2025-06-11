using ADAShop.Shared.Emuns;
using ADAShop.Shared.Entities;
using ADAShop.Web.Models;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.CartItem;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ADAShop.Web.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;

        public CartItemController(ICartItemService cartItemService, ICartService cartService)
        {
            _cartItemService = cartItemService;
            _cartService = cartService;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            ClaimsIdentityModel? identitySession = null;
            var claimsIdentity = HttpContext.Session.Get("ClaimsIdentityModel");
            if (claimsIdentity == null)
            {
                return View("Login", "Account");
            }

            identitySession = JsonSerializer.Deserialize<ClaimsIdentityModel>(claimsIdentity)!;
            ViewBag.ClaimsIdentityViewBag = identitySession;
            var carts = await _cartService.GetByUserIdAsync(identitySession.UserId);
            var cart = carts.Where(x => x.Status == ShoppingCartStatusEnum.NEW.ToString()).LastOrDefault();
            string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            ViewBag.UserId = userIdClaim;
            return View(cart! != null ? cart!.CartItems : new List<CartItem>());
        }

        [HttpPost(nameof(Insert))]
        public async Task<IActionResult> Insert(CartItem cartItem)
        {
            CartItem response = await _cartItemService.CreateAsync(cartItem);
            return View(response);
        }
    }
}