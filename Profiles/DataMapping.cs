using AutoMapper;
using LaundryAPI.Dtos;
using LaundryAPI.Models;

namespace LaundryAPI.Profiles
{
    public class DataMapping : Profile
    {
        public DataMapping()
        {
            CreateMap<SignDto, UserProfile>();
            CreateMap<AddressWriteDto, AddressLine>();
            CreateMap<AddressLine, AddressWriteDto>();
            CreateMap<AddressLine, AddressReadDto>();
            CreateMap<AddressWriteDto, AddressReadDto>();
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderWriteDto, Order>();
            CreateMap<OrderWriteDto, OrderReadDto>();
            CreateMap<UserProfile, UserReadDto>();
            CreateMap<UserWriteDto, UserProfile>();
        }
    }
}
