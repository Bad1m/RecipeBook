using AutoMapper;
using ReviewMicroservice.Domain.Dtos;
using ReviewMicroservice.Domain.Entities;

namespace ReviewMicroservice.Domain.Mappings
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