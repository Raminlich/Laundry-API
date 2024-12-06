using System.ComponentModel.DataAnnotations;

namespace LaundryAPI.Dtos
{
    public class OrderWriteDto
    {
        [Required]
        public int PickUpTime { get; set; }
        [Required]
        public int DeliveryTime { get; set; }
        [Required]
        public DateTime PickUpDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public int PickUpAddressId { get; set; }
        [Required]
        public int DeliveryAddressId { get; set; }

    }
}
