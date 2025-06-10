using ADAShop.Shared.Entities;
using ADAShop.Web.Services.CartItem;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Web.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CartItem cartItem)
        {
            CartItem response = await _cartItemService.CreateAsync(cartItem);
            return View(response);
        }
    }
}