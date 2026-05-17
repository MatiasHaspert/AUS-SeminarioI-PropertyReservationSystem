using AutoMapper;
using Domain.ValueObjects;
using Application.DTOs;

namespace Application.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
