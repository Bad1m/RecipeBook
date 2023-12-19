using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.DataAccess.Models;
using AutoMapper;

namespace AuthMicroservice.BusinessLogic.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}