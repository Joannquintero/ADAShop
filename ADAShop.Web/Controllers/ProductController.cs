using ADAShop.Shared.Entities;
using ADAShop.Web.Services.Cart;
using ADAShop.Web.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Web.Controllers
{
    public class ProductController : Controller
    {
        public ProductController()
        {

        }


        public IActionResult AddtoCart(int productId, long userId, int quantity = 1)
        {
            //List<Cart> carts = cartService.GetAll("CartItems");
            //Product product = productService.Get(id);
            //if (carts.Count == 0) // the first product added to cart => then create an insatace from cart
            //{
            //    Cart cart = new Cart() { CartItems = new List<CartItem>() };

            //    cartService.Insert(cart);
            //    cartService.Save();

            //    CartItem cartItem = new CartItem()
            //    {
            //        Quantity = quantity,

            //        ProductId = id,
            //        Product = product,

            //        CartId = cart.Id,
            //        Cart = cart,
            //    };

            //    cartItemService.Insert(cartItem);
            //    cartItemService.Save();
            //    cartService.Save();
            //    return RedirectToAction("Details", "Product", new { id = id });
            //}
            //else
            //{
            //    Cart cart = cartService.GetAll("CartItems").FirstOrDefault();
            //    CartItem? existedItem = cart.CartItems.FirstOrDefault(c => c.ProductId == id);
            //    if (existedItem != null)
            //    {
            //        existedItem.Quantity += quantity;
            //    }
            //    else
            //    {
            //        CartItem cartItem = new CartItem()
            //        {
            //            Quantity = quantity,
            //            ProductId = id,
            //            Product = product,
            //            CartId = cart.Id,
            //            Cart = cart,
            //        };
            //        cartItemService.Insert(cartItem);

            //    }
            //    cartItemService.Save();
            //    cartService.Save();
            //    return RedirectToAction("Details", "Product", new { id = id });
            //}

            return View();
        }
    }
}
