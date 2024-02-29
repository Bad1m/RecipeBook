using AutoMapper;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}