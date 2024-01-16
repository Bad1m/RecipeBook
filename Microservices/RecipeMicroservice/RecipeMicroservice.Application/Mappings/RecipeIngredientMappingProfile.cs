using AutoMapper;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Application.Mappings
{
    public class RecipeIngredientMappingProfile : Profile
    {
        public RecipeIngredientMappingProfile()
        {
            CreateMap<CreateRecipeIngredientCommand, RecipeIngredient>();

            CreateMap<RecipeIngredient, RecipeIngredientDto>();

            CreateMap<CreateIngredientForRecipeCommand, Ingredient>();
        }
    }
}