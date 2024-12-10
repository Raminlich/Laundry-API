using AutoMapper;
using Laundry_API.Services;
using LaundryAPI.Data;
using LaundryAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaundryAPI.Controllers
{
    [Route("/orders")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(UserDbContext userDbContext, OrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<OrderReadDto>> GetOrders()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _orderService.GetOrders(userId);
            return Ok(orders.Data);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> SubmitOrder([FromBody] OrderWriteDto order)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderRead = await _orderService.PlaceOrder(userId, order);
            return Ok(orderRead.Data);
        }
    }
}
