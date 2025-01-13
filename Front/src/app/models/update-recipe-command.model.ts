import { UserDto } from "./user.model";

export interface UpdateRecipeCommand {
    id: number;
    dish: string;
    category: string;
    description: string;
    prepTime: string; 
    img: string; 
    user?: UserDto;
  }