using System.ComponentModel.DataAnnotations;

namespace LaundryAPI.Dtos
{
    public class SignDto
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}
