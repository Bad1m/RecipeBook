﻿using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Create
{
    public class CreateRecipeIngredientCommand : IRequest<RecipeIngredientDto>
    {
        public double Amount { get; set; }
        public CreateIngredientCommand Ingredient { get; set; }
    }
}