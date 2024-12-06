using System.ComponentModel.DataAnnotations;

namespace LaundryAPI.Models
{
    public class AddressLine
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int HouseNumber { get; set; }
        [Required]
        public int District { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
