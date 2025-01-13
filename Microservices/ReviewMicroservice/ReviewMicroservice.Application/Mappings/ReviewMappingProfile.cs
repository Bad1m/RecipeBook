using AutoMapper;
using ReviewMicroservice.Application.Dtos;
using ReviewMicroservice.Domain.Entities;

namespace ReviewMicroservice.Application.Mappings
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<ReviewDto, Review>().ReverseMap();

            CreateMap<ReviewRequest, Review>().ReverseMap();

            CreateMap<ReviewDto, ReviewRequest>().ReverseMap();
        }
    }
}