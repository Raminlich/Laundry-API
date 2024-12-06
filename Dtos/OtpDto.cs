using System.ComponentModel.DataAnnotations;

namespace LaundryAPI.Dtos
{
    public class OtpDto
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int Otp {  get; set; }
    }
}
