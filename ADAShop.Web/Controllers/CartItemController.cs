using ADAShop.Shared.Entities;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.CartItem;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            //TODO: [JAN] - Summary
            var carts = await _cartService.GetByUserIdAsync(3);
            var cart = carts.LastOrDefault();

            //ViewBag.Cart = cartService.GetAll("CartItems").FirstOrDefault();
            string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            ViewBag.UserId = userIdClaim;
            return View(cart!.CartItems != null ? cart!.CartItems : new List<CartItem>());
        }

        [HttpPost(nameof(Insert))]
        public async Task<IActionResult> Insert(CartItem cartItem)
        {
            CartItem response = await _cartItemService.CreateAsync(cartItem);
            return View(response);
        }
    }
}