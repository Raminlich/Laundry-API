using LaundryAPI.Data;
using LaundryAPI.Dtos;
using LaundryAPI.Models;
using LaundryAPI.Shared;

namespace LaundryAPI.Services
{
    public class SignInService
    {
        private readonly UserDbContext _userDbContext;
        private readonly TokenService _tokenService;

        public SignInService(UserDbContext dbContext, TokenService tokenService)
        {
            _userDbContext = dbContext;
            _tokenService = tokenService;
        }


        public void SignIn(SignDto signInDto)
        {
            //Send Otp to signInDto.PhoneNumber
        }

        public OperationResult<string> VerifyOTP(OtpDto otpDto)
        {
            var userProfile = _userDbContext.Profiles.FirstOrDefault(p => p.PhoneNumber == otpDto.PhoneNumber);
            var oResult = new OperationResult<string>();
            if (userProfile == null)
            {
                oResult = SignUpUser(otpDto).Result;
            }
            else
            {
                oResult = SignInUser(otpDto);
            }
            return oResult;
        }

        private async Task<OperationResult<string>> SignUpUser(OtpDto otpDto)
        {
            if (otpDto.Otp == 1111)
            {
                var userProfile = new UserProfile();
                userProfile.PhoneNumber = otpDto.PhoneNumber;
                userProfile.UserID = Guid.NewGuid().ToString();
                var token = _tokenService.GenerateToken(userProfile.UserID);
                _userDbContext.Profiles.Add(userProfile);
                await _userDbContext.SaveChangesAsync();
                var sResult = new OperationResult<string>(true, token);
                return sResult;
            }
            var fResult = new OperationResult<string>(false, null,"Invalid OTP!");
            return fResult;
        }

        private OperationResult<string> SignInUser(OtpDto otpDto)
        {
            var userProfile = _userDbContext.Profiles.FirstOrDefault(p => p.PhoneNumber == otpDto.PhoneNumber);
            if (otpDto.Otp == 1111)
            {
                var token = _tokenService.GenerateToken(userProfile.UserID);
                var sResult = new OperationResult<string>(true, token);
                return sResult;
            }
            var fResult = new OperationResult<string>(false, null, "Invalid OTP!");
            return fResult;
        }
    }
}
