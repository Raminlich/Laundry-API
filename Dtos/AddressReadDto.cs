using System.ComponentModel.DataAnnotations;

namespace LaundryAPI.Dtos
{
    public class  AddressReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int HouseNumber { get; set; }
        public int District { get; set; }
    }
}
