using LaundryAPI.Data;
using LaundryAPI.Dtos;
using LaundryAPI.Filters;
using LaundryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaundryAPI.Controllers
{
    [Route("/signin")]
    public class SignInController : Controller
    {
        private readonly SignInService _signInService;

        public SignInController(SignInService signInService)
        {
            _signInService = signInService;
        }

        [HttpPost]
        public ActionResult SignIn(SignDto signInDto)
        {
            _signInService.SignIn(signInDto);
            return Ok();
        }

        [HttpPost("otp")]
        public ActionResult VerifySignIn([FromBody] OtpDto otpData)
        {
            var result = _signInService.VerifyOTP(otpData);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}
