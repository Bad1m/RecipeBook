export interface Ingredient {
    id: number;
    name: string;
    amount: number;
    unit: string;
  }
  
  export interface CreateIngredientForRecipeCommand {
    name: string;
    amount: number;
    unit: string;
    recipeId: number;
  }
  
  export interface UpdateIngredientCommand {
    id: number;
    name: string;
    unit: string;
    amount: number;
    ingredient?: Ingredient
  }