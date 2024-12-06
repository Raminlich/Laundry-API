using LaundryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaundryAPI.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        private readonly TokenService _tokenService;

        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpPost("validate")]
        public ActionResult ValidateToken()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateToken()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var token = _tokenService.GenerateToken(userId);
            return Ok(token);
        }
    }
}
