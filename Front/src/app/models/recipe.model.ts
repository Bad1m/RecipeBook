import { UserDto } from "./user.model";
import { Ingredient } from "./ingredient.model";

export interface Recipe {
  id: number;
  dish: string;
  category: string;
  description: string;
  prepTime: string;
  img: string;
  user: UserDto;
  recipeIngredients?: RecipeIngredient[];
}

export interface RecipeIngredient {
  amount: number;
  ingredient: Ingredient;
}