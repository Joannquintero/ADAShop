using ADAShop.Shared.Entities;
using ADAShop.Web.Services.Account;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.CartItem;
using ADAShop.Web.Services.Category;
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

        public OrderController(IProductService productService, ICartService cartService, IAccountService accountService)
        {
            _productService = productService;
            _cartService = cartService;
            _accountService = accountService;
        }

        //TODO: [JAN] - Summary
        public async Task<IActionResult> Checkout(int CartId, int UserId = 3)
        {
            List<Product> products = await _productService.GetAllAsync();
            ViewData["AllProductsNames"] = products.Select(c => c.Name).ToList();

            HttpContext.Session.SetInt32("uId", UserId);
            HttpContext.Session.SetInt32("cId", CartId);

            //List<CartItem> items = cartItemService.Get(i => i.CartId == CartId);
            var carts = await _cartService.GetByUserIdAsync(UserId);

            List<CartItem> items = carts.FirstOrDefault()!.CartItems!;
            User? user = await _accountService.GetAsync("johns");
            if (user != null)
            {
                Order order = new Order()
                {
                    UserId = user.Id,
                    OrderDate = DateTime.Now,
                    OrderItems = new List<OrderItem>()
                };

                foreach (var item in items)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        ProductId = item.ProductId,
                        OrderId = order.Id,
                        Quantity = item.Quantity
                    };
                    order.OrderItems.Add(orderItem);
                    //orderItemService.Insert(orderItem);
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
                return Json("Error");
            }
            return Json("Error");
        }
    }
}
