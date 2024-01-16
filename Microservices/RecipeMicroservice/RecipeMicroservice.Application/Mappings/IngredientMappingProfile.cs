using AutoMapper;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Application.Mappings
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            CreateMap<CreateIngredientForRecipeCommand, Ingredient>();

            CreateMap<CreateIngredientCommand, Ingredient>();

            CreateMap<UpdateIngredientCommand, Ingredient>();

            CreateMap<Ingredient, IngredientDto>();
        }
    }
}