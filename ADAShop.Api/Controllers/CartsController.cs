using ADAShop.Api.Repository.Cart;
using ADAShop.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAll()
        {
            var response = await _cartRepository.GetAllAsync();
            return Ok(response);
        }

        [Authorize(Roles = "User")]
        public async Task<ActionResult> GetByUserId(long userId)
        {
            var response = await _cartRepository.GetByUserIdAsync(userId);
            return Ok(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("User")]
        public async Task<ActionResult> Insert(Cart cart)
        {
            var response = await _cartRepository.CreateAsync(cart);
            return Ok(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("User")]
        public async Task<ActionResult> InsertCartItem(CartItem cartItem)
        {
            var response = await _cartRepository.CreateCartItemAsync(cartItem);
            return Ok(response);
        }
    }
}