using ADAShop.Shared.Emuns;
using ADAShop.Shared.Entities;
using ADAShop.Web.Models;
using ADAShop.Web.Services.Account;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.CartItem;
using ADAShop.Web.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        public async Task<JsonResult> AddtoCart(int productId, int quantity)
        {
            ClaimsIdentityModel? identitySession = null;
            var claimsIdentity = HttpContext.Session.Get("ClaimsIdentityModel");
            if (claimsIdentity == null)
            {
                return Json(null);
            }

            identitySession = JsonSerializer.Deserialize<ClaimsIdentityModel>(claimsIdentity)!;
            ViewBag.ClaimsIdentityViewBag = identitySession;

            var carts = await _cartService.GetByUserIdAsync(identitySession!.UserId);
            carts = carts.Where(x => x.Status == ShoppingCartStatusEnum.NEW.ToString()).ToList();
            Product product = await _productService.GetByIdAsync(productId);
            if (carts.Count == 0)
            {
                Cart cart = new Cart();
                var user = await _accountService.GetAsync(identitySession!.UserName!);
                cart.UserId = user.Id;
                cart.Status = ShoppingCartStatusEnum.NEW.ToString();

                var cartResponse = await _cartService.CreateAsync(cart);
                CartItem cartItem = new CartItem()
                {
                    Quantity = quantity,
                    ProductId = productId,
                    CartId = cartResponse.Id,
                };

                var cartItemResponse = await _cartItemService.CreateAsync(cartItem);
                return Json(cartItemResponse);
            }
            else
            {
                var cart = carts.Where(x => x.Status == ShoppingCartStatusEnum.NEW.ToString()).FirstOrDefault();
                if (cart != null)
                {
                    CartItem? existedItem = cart.CartItems!.FirstOrDefault(c => c.ProductId == productId);
                    if (existedItem != null)
                    {
                        existedItem.Quantity += quantity;
                        existedItem.Product = null;
                        var cartItemResponse = await _cartItemService.UpdateAsync(existedItem);
                        return Json(cartItemResponse);
                    }
                    else
                    {
                        CartItem cartItem = new CartItem()
                        {
                            Quantity = quantity,
                            ProductId = productId,
                            CartId = cart.Id,
                        };

                        var cartItemResponse = await _cartItemService.CreateAsync(cartItem);
                        return Json(cartItemResponse);
                    }
                }
                return Json(carts);
            }
        }
    }
}