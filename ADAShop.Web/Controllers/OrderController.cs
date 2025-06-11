using ADAShop.Shared.DTOs;
using ADAShop.Shared.Entities;
using ADAShop.Web.Models;
using ADAShop.Web.Services.Account;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.Order;
using ADAShop.Web.Services.OrderItem;
using ADAShop.Web.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ADAShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IAccountService _accountService;
        private readonly IOrdenService _ordenService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(
            IProductService productService,
            ICartService cartService,
            IAccountService accountService,
            IOrdenService ordenService,
            IOrderItemService orderItemService)
        {
            _productService = productService;
            _cartService = cartService;
            _accountService = accountService;
            _ordenService = ordenService;
            _orderItemService = orderItemService;
        }

        public async Task<IActionResult> Checkout(int CartId)
        {
            ClaimsIdentityModel? identitySession = null;
            var claimsIdentity = HttpContext.Session.Get("ClaimsIdentityModel");
            if (claimsIdentity == null)
            {
                return Json("Error");
            }

            identitySession = JsonSerializer.Deserialize<ClaimsIdentityModel>(claimsIdentity)!;
            ViewBag.ClaimsIdentityViewBag = identitySession;

            List<Product> products = await _productService.GetAllAsync();
            ViewData["AllProductsNames"] = products.Select(c => c.Name).ToList();
            var cartResponse = await _cartService.GetByIdAsync(CartId);

            List<CartItem> cartItems = cartResponse.CartItems!;
            User? user = await _accountService.GetAsync(identitySession.UserName!);
            if (user != null)
            {
                Order order = new Order()
                {
                    UserId = user.Id,
                    OrderItems = new List<OrderItem>()
                };

                var response = await _ordenService.CreateAsync(order);
                if (response != null)
                {
                    foreach (var cart in cartItems)
                    {
                        var product = products.Where(x => x.Id == cart.ProductId).FirstOrDefault();
                        if (product != null)
                        {
                            OrderItemDTO orderItem = new OrderItemDTO()
                            {
                                ProductId = cart.ProductId,
                                OrderId = response.Id,
                                Stock = product!.Stock,
                                Quantity = cart.Quantity,
                                Amount = cart.Product!.Price * cart.Quantity,
                                CartId = cart.Id
                            };

                            var responseOrderItem = await _orderItemService.CreateAsync(orderItem);
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}