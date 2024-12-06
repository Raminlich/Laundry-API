using AutoMapper;
using LaundryAPI.Data;
using LaundryAPI.Dtos;
using LaundryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LaundryAPI.Controllers
{
    [Route("/orders")]
    public class OrderController : Controller
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public OrderController(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userDbContext.Profiles.AsNoTracking()
                .Include(u => u.Orders)
                .ThenInclude(o => o.PickUpAddress)
                .Include(u => u.Orders)
                .ThenInclude(o => o.DeliveryAddress)
                .FirstOrDefault(p => p.UserID == userId);
            var orders = _mapper.Map<IEnumerable<OrderReadDto>>(user.Orders);

            return Ok(orders);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderWriteDto>> SubmitOrder([FromBody] OrderWriteDto order)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userDbContext.Profiles.FirstOrDefault
                  (p => p.UserID == userId);
            var cOrder = _mapper.Map<Order>(order);
            cOrder.UserId = user.Id;
            cOrder.IsActive = 1;
            _userDbContext.Orders.Add(cOrder);
            await _userDbContext.SaveChangesAsync();
            var orderRead = _mapper.Map<OrderReadDto>(order);
            return Ok(orderRead);
        }
    }
}
