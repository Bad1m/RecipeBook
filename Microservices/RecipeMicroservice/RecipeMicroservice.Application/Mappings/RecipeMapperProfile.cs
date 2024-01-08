using AutoMapper;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Application.Mappings
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<CreateRecipe, Recipe>();

            CreateMap<UpdateRecipe, Recipe>();

            CreateMap<UpdateInstruction, Instruction>();

            CreateMap<CreateRecipeIngredient, RecipeIngredient>();

            CreateMap<CreateInstructionForRecipe, Instruction>();

            CreateMap<CreateIngredient, Ingredient>();

            CreateMap<CreateInstruction, Instruction>();

            CreateMap<CreateIngredient, Ingredient>();

            CreateMap<UpdateIngredient, Ingredient>();

            CreateMap<Ingredient, IngredientDto>();

            CreateMap<Recipe, RecipeDto>();

            CreateMap<CreateIngredientForRecipe, Ingredient>(); 

            CreateMap<RecipeIngredient, RecipeIngredientDto>();

            CreateMap<Instruction, InstructionDto>();
        }
    }
}