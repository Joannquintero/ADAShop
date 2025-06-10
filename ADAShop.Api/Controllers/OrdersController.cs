using ADAShop.Api.Repository.Order;
using ADAShop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost(nameof(Insert))]
        //[ValidateAntiForgeryToken]
        //[Authorize("User")]
        public async Task<ActionResult> Insert(Order order)
        {
            var response = await _orderRepository.CreateAsync(order);
            return Ok(response);
        }

        [HttpGet(nameof(GetAll))]
        //[Authorize("Admin")]
        public async Task<ActionResult> GetAll()
        {
            var response = await _orderRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpPost(nameof(InsertOrderItem))]
        //[ValidateAntiForgeryToken]
        //[Authorize("User")]
        public async Task<ActionResult> InsertOrderItem(OrderItem orderItem)
        {
            var response = await _orderRepository.CreateOrderItemAsync(orderItem);
            return Ok(response);
        }
    }
}