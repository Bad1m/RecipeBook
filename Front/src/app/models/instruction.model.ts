export interface Instruction {
    id: number;
    stepNumber: number;
    description: string;
  }

  export interface CreateInstructionForRecipeCommand {
    stepNumber: number;
    description: string;
    recipeId: number;
  }

  export interface UpdateInstructionCommand {
    id: number;
    stepNumber: number;
    description: string;
  }