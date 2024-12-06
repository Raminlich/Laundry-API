using System.ComponentModel.DataAnnotations;

namespace LaundryAPI.Dtos
{
    public class AddressWriteDto
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int HouseNumber { get; set; }
        [Required]
        public int District { get; set; }
    }
}
