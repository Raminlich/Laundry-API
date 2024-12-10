using AutoMapper;
using LaundryAPI.Data;
using LaundryAPI.Dtos;
using LaundryAPI.Models;
using LaundryAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace Laundry_API.Services
{
    public class OrderService
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public OrderService(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<OrderReadDto>> PlaceOrder(string userId, OrderWriteDto order)
        {
            var user = _userDbContext.Profiles.FirstOrDefault
                    (p => p.UserID == userId);
            var cOrder = _mapper.Map<Order>(order);
            cOrder.UserId = user.Id;
            cOrder.IsActive = 1;
            _userDbContext.Orders.Add(cOrder);
            await _userDbContext.SaveChangesAsync();
            var orderRead = _mapper.Map<OrderReadDto>(order);
            var result = new OperationResult<OrderReadDto>(true, orderRead);
            return result;
        }

        public OperationResult<IEnumerable<OrderReadDto>> GetOrders(string userId)
        {
            var user = _userDbContext.Profiles.AsNoTracking()
                .Include(u => u.Orders)
                .ThenInclude(o => o.PickUpAddress)
                .Include(u => u.Orders)
                .ThenInclude(o => o.DeliveryAddress)
                .FirstOrDefault(p => p.UserID == userId);
            var orders = _mapper.Map<IEnumerable<OrderReadDto>>(user.Orders);
            var result = new OperationResult<IEnumerable<OrderReadDto>>(true, orders);
            return result;
        }
    }
}
