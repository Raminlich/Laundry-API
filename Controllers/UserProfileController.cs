using AutoMapper;
using LaundryAPI.Data;
using LaundryAPI.Dtos;
using LaundryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaundryAPI.Controllers
{
    [Route("/user")]
    public class UserProfileController : Controller
    {
        private readonly UserDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserProfileController(UserDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<UserReadDto> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Profiles.FirstOrDefault(p => p.UserID == userId);
            var response = _mapper.Map<UserReadDto>(user);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUserInfo([FromBody] UserWriteDto userDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Profiles.FirstOrDefault(p => p.UserID == userId);
            _mapper.Map(userDto, user);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
