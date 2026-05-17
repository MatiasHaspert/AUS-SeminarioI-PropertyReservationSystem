using AutoMapper;
using Domain.Entities;
using Application.DTOs.Review;

namespace Application.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponseDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<ReviewRequestDTO, Review>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
