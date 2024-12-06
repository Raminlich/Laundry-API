using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using LaundryAPI.Data;
using LaundryAPI.Dtos;
using LaundryAPI.Models;

namespace LaundryAPI.Controllers
{
    [Route("/addresses")]
    public class UserAddressController :  Controller
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public UserAddressController(UserDbContext userDbContext , IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AddressReadDto>> SubmitAddress([FromBody] AddressWriteDto addressDto)
        {
            var address = _mapper.Map<AddressLine>(addressDto);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userDbContext.Profiles.FirstOrDefault
                (p => p.UserID == userId );
            address.UserId = user.Id;
            _userDbContext.Addresses.Add(address);
            await _userDbContext.SaveChangesAsync();
            var addressRead = _mapper.Map<AddressReadDto>(addressDto);
            return Ok(addressDto);
        }


        [Authorize]
        [HttpGet]
        public ActionResult GetUserAddresses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userDbContext.Profiles.AsNoTracking().
                Include(p => p.AddressLines).
                FirstOrDefault(p => p.UserID == userId);
            var addressLines = user.AddressLines;
            var activeAddresses = addressLines.Where(a => a.IsActive == true);
            var addressesDto = _mapper.Map<IEnumerable<AddressReadDto>>(activeAddresses);
            return Ok(addressesDto);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteUserAddress(int addressId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userDbContext.Profiles.Include(p => p.AddressLines)
                .FirstOrDefault(p => p.UserID == userId);
            var address = user.AddressLines.FirstOrDefault(a => a.Id == addressId);
            address.IsActive = false;
            await _userDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
