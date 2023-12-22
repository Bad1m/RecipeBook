using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.DataAccess.Entities;
using AutoMapper;

namespace AuthMicroservice.BusinessLogic.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegistrationDto, User>();

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}