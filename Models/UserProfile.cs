using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LaundryAPI.Models
{
    public class UserProfile
    {
        [Key]
        [Required]
        public int Id { get; private set; }
        [Required]
        public string UserID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [AllowNull]
        public ICollection<AddressLine> AddressLines { get; set; }
        [AllowNull]
        public ICollection<Order> Orders { get; set; }
    }
}
