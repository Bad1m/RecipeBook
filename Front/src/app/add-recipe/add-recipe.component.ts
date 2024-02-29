import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeService } from '../services/recipe.service';
import { CreateRecipeCommand } from '../models/create-recipe-command.model';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss'],
})
export class AddRecipeComponent {
  recipe: CreateRecipeCommand = {
    dish: '',
    category: '',
    description: '',
    prepTime: '',
    img: '',
    userName: this.getUserNameFromLocalStorage(),
    recipeIngredients: [],
    instructions: []
  };

  loading: boolean = false;
  imageError: boolean = false;

  constructor(
    private recipeService: RecipeService,
    private router: Router
  ) {}

  handleImageError() {
    this.imageError = true;
  }

  clearImageError() {
    this.imageError = false;
  }

  addRecipe() {
    this.recipeService.createRecipe(this.recipe).subscribe(() => {
      this.loading = true;
      this.router.navigate(['/personal-account']);
    });
  }

  addIngredient() {
    this.recipe.recipeIngredients?.push({ amount: 0, ingredient: { name: '', unit: '' } });
  }

  removeIngredient(index: number) {
    this.recipe.recipeIngredients?.splice(index, 1);
  }

  addInstruction() {
    const stepNumber = (this.recipe.instructions?.length ?? 0) + 1;
    this.recipe.instructions?.push({ stepNumber, description: '' });
  }

  removeInstruction(index: number) {
    this.recipe.instructions?.splice(index, 1);
  }

  private getUserNameFromLocalStorage(): string {
    if (typeof localStorage !== 'undefined') {
      return localStorage.getItem('userName') ?? '';
    } else {
      return '';
    }
  }
}