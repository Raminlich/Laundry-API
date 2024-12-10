namespace LaundryAPI.Dtos
{
    public class OrderReadDto
    {
        public string OrderNumber { get; set; }
        public int PickUpTime { get; set; }
        public int DeliveryTime { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public AddressReadDto? PickUpAddress { get; set; }
        public AddressReadDto? DeliveryAddress { get; set; }
        public int IsActive { get; set; }
    }
}
