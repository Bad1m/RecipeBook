using AutoMapper;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Application.Mappings
{
    public class RecipeMappingProfile : Profile
    {
        public RecipeMappingProfile()
        {
            CreateMap<CreateRecipeCommand, Recipe>();

            CreateMap<UpdateRecipeCommand, Recipe>().ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Recipe, RecipeDto>();
        }
    }
}