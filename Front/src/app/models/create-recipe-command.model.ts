export interface CreateRecipeCommand {
    dish?: string;
    category?: string;
    description?: string;
    prepTime: string;
    img?: string;
    userName?: string;
    recipeIngredients?: CreateRecipeIngredientCommand[];
    instructions?: CreateInstructionCommand[];
  }

  export interface CreateRecipeIngredientCommand {
    amount: number;
    ingredient: CreateIngredientCommand;
  }
  
  export interface CreateIngredientCommand {
    name: string;
    unit: string;
  }
  
  export interface CreateInstructionCommand {
    stepNumber: number;
    description: string;
  }