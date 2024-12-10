using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaundryAPI.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; private set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PickUpTime { get; set; }
        [Required]
        public int DeliveryTime { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime PickUpDate { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public int PickUpAddressId { get; set; }
        public AddressLine PickUpAddress { get; set; }
        [Required]
        public int DeliveryAddressId { get; set; }
        public AddressLine DeliveryAddress { get; set; }
        [Required]
        public int IsActive { get; set; }

    }
}
