using ADAShop.Api.Helpers;
using ADAShop.Shared.Entities;
using ADAShop.Web.Services.Account;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.CartItem;
using ADAShop.Web.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ICartItemService _cartItemService;
        private readonly IAccountService _accountService;

        public CartController(
            ICartService cartService, 
            IProductService productService, 
            ICartItemService cartItemService,
            IAccountService accountService)
        {
            _cartService = cartService;
            _productService = productService;
            _cartItemService = cartItemService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(long userId)
        {
            var carts = await _cartService.GetByUserIdAsync(userId);
            return View(carts);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Cart cart)
        {
            Cart carts = await _cartService.CreateAsync(cart);
            return View(cart);
        }

        public async Task<IActionResult> AddtoCart(int productId, int quantity = 1)
        {
            //TODO: [JAN] - Summary
            var carts = await _cartService.GetByUserIdAsync(3);
            Product product = await _productService.GetByIdAsync(productId);
            if (carts.Count == 0)
            {
                Cart cart = new Cart();
                var user = await _accountService.GetAsync("johns");
                cart.UserId = user.Id;
                var cartResponse = await _cartService.CreateAsync(cart);
                CartItem cartItem = new CartItem()
                {
                    Quantity = quantity,
                    ProductId = productId,
                    CartId = cartResponse.Id,
                };

                var cartItemResponse = await _cartItemService.CreateAsync(cartItem);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Cart cart = cartService.GetAll("CartItems").FirstOrDefault();
                //CartItem? existedItem = cart.CartItems.FirstOrDefault(c => c.ProductId == id);
                //if (existedItem != null)
                //{
                //    existedItem.Quantity += quantity;
                //}
                //else
                //{
                //    CartItem cartItem = new CartItem()
                //    {
                //        Quantity = quantity,
                //        ProductId = id,
                //        Product = product,
                //        CartId = cart.Id,
                //        Cart = cart,
                //    };
                //    cartItemService.Insert(cartItem);
                //}
                //cartItemService.Save();
                //cartService.Save();
                //return RedirectToAction("Details", "Product", new { id = id });
                return View(carts);
            }
        }
    }
}