using ADAShop.Api.Repository.Order;
using ADAShop.Shared.DTOs;
using ADAShop.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost(nameof(Insert))]
        public async Task<ActionResult> Insert(Order order)
        {
            var response = await _orderRepository.CreateAsync(order);
            return Ok(response);
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult> GetAll()
        {
            string userName = User.Identity!.Name!;
            var response = await _orderRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpPost(nameof(InsertOrderItem))]
        public async Task<ActionResult> InsertOrderItem(OrderItemDTO orderItemDTO)
        {
            var response = await _orderRepository.CreateOrderItemAsync(orderItemDTO);
            return Ok(response);
        }
    }
}