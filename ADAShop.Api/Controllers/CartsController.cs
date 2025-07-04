﻿using ADAShop.Api.Repository.Cart;
using ADAShop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult> GetAll()
        {
            // ⚠️ throw new Exception("¡Algo salió mal!")
            var response = await _cartRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet(nameof(GetByUserId))]
        public async Task<ActionResult> GetByUserId(long userId)
        {
            var response = await _cartRepository.GetByUserIdAsync(userId);
            return Ok(response);
        }

        [HttpGet(nameof(GetById))]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await _cartRepository.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost(nameof(Insert))]
        public async Task<ActionResult> Insert(Cart cart)
        {
            var response = await _cartRepository.CreateAsync(cart);
            return Ok(response);
        }

        [HttpPost(nameof(Update))]
        public async Task<ActionResult> Update(Cart cart)
        {
            var response = await _cartRepository.UpdateAsync(cart);
            return Ok(response);
        }

        [HttpPost(nameof(InsertCartItem))]
        public async Task<ActionResult> InsertCartItem(CartItem cartItem)
        {
            var response = await _cartRepository.CreateCartItemAsync(cartItem);
            return Ok(response);
        }

        [HttpPut(nameof(UpdateCartItem))]
        public async Task<ActionResult> UpdateCartItem(CartItem cartItem)
        {
            var response = await _cartRepository.UpdateItemAsync(cartItem);
            return Ok(response);
        }
    }
}