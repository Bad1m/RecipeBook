import { Injectable } from '@angular/core';
import { RecipeClient } from '../clients/recipe.client';
import { Observable } from 'rxjs';
import { Recipe } from '../models/recipe.model';
import { PaginationSettings } from '../models/pagination-settings.model';
import { CreateRecipeCommand } from '../models/create-recipe-command.model';
import { UpdateRecipeCommand } from '../models/update-recipe-command.model';
import { PaginatedResult } from '../models/pagination-settings.model';

@Injectable({
  providedIn: 'root',
})
export class RecipeService {
  constructor(private recipeClient: RecipeClient) {}

  public getAllRecipes(paginationSettings: PaginationSettings): Observable<PaginatedResult<Recipe>> {
    return this.recipeClient.getAllRecipes(paginationSettings);
  }

  public getRecipesByUserName(userName: string, paginationSettings: PaginationSettings): Observable<PaginatedResult<Recipe>> {
    return this.recipeClient.getRecipesByUserName(userName, paginationSettings);
  }

  public getRecipeById(id: number): Observable<Recipe> {
    return this.recipeClient.getRecipeById(id);
  }

  public createRecipe(command: CreateRecipeCommand): Observable<Recipe> {
    return this.recipeClient.createRecipe(command);
  }

  public updateRecipe(command: UpdateRecipeCommand): Observable<Recipe> {
    return this.recipeClient.updateRecipe(command);
  }

  public deleteRecipe(id: number): Observable<void> {
    return this.recipeClient.deleteRecipe(id);
  }
}