using ADAShop.Shared.Entities;
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
        private readonly OrderItemService _orderItemService;

        public OrderController(
            IProductService productService,
            ICartService cartService,
            IAccountService accountService,
            IOrdenService ordenService,
            OrderItemService orderItemService)
        {
            _productService = productService;
            _cartService = cartService;
            _accountService = accountService;
            _ordenService = ordenService;
            _orderItemService = orderItemService;
        }

        //TODO: [JAN] - Summary
        public async Task<IActionResult> Checkout(int CartId, int UserId = 3)
        {
            List<Product> products = await _productService.GetAllAsync();
            ViewData["AllProductsNames"] = products.Select(c => c.Name).ToList();

            HttpContext.Session.SetInt32("uId", UserId);
            HttpContext.Session.SetInt32("cId", CartId);
            var carts = await _cartService.GetByIdAsync(CartId);

            List<CartItem> cartItems = carts.CartItems!;
            User? user = await _accountService.GetAsync("johns");
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
                        OrderItem orderItem = new OrderItem()
                        {
                            ProductId = cart.ProductId,
                            OrderId = order.Id,
                            Quantity = cart.Quantity,
                            Amount = cart.Product!.Price * cart.Quantity,
                        };

                        var responseOrderItem = await _orderItemService.CreateAsync(orderItem);
                    }
                    var serializedRecords = JsonSerializer.Serialize(order);
                    HttpContext.Session.SetString("order", serializedRecords);

                    //Shipment shipment = new Shipment() { Date = DateTime.Now.AddDays(3) };

                    //    List<Cart>? carts = _cartService.GetAll();

                    //    if (carts.Count == 0)
                    //    {
                    //        Cart cart = new Cart() { CartItems = new List<CartItem>() };

                    //        CheckoutViewModel checkoutVM = new CheckoutViewModel() { };

                    //        checkoutVM.Date = shipment.Date;
                    //        checkoutVM.Categories = categoryService.GetAll();
                    //        checkoutVM.Cart = cart;

                    //        decimal total = 0;
                    //        foreach (OrderItem item in order.OrderItems)
                    //        {
                    //            item.Product = productService.Get(item.ProductId);
                    //            total += item.Product.Price * item.Quantity;
                    //        }
                    //        ViewBag.order = order;
                    //        ViewBag.total = total;

                    //        return View(checkoutVM);
                    //    }
                    //    else
                    //    {
                    //        CheckoutViewModel checkoutVM = new CheckoutViewModel() { };

                    //        checkoutVM.Date = shipment.Date;
                    //        checkoutVM.Categories = categoryService.GetAll();
                    //        checkoutVM.Cart = _cartService.GetAll("CartItems").FirstOrDefault();

                    //        decimal total = 0;
                    //        foreach (OrderItem item in order.OrderItems)
                    //        {
                    //            item.Product = productService.Get(item.ProductId);
                    //            total += item.Product.Price * item.Quantity;
                    //        }
                    //        ViewBag.order = order;
                    //        ViewBag.total = total;

                    //        return View(checkoutVM);
                    //    }
                    //}
                }
                return Json("Error");
            }
            return Json("Error");
        }
    }
}