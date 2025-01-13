import { Injectable } from '@angular/core';
import { IngredientClient } from '../clients/ingredient.client';
import { Observable } from 'rxjs';
import { Ingredient } from '../models/ingredient.model';
import { CreateIngredientForRecipeCommand } from '../models/ingredient.model';
import { UpdateIngredientCommand } from '../models/ingredient.model';

@Injectable({
  providedIn: 'root',
})
export class IngredientService {
  constructor(private ingredientClient: IngredientClient) {}

  public getIngredientsByRecipeId(recipeId: number): Observable<Ingredient[]> {
    return this.ingredientClient.getIngredientsByRecipeId(recipeId);
  }

  public createIngredient(command: CreateIngredientForRecipeCommand): Observable<Ingredient> {
    return this.ingredientClient.createIngredient(command);
  }

  public updateIngredient(command: UpdateIngredientCommand): Observable<Ingredient> {
    return this.ingredientClient.updateIngredient(command);
  }

  public deleteIngredient(id: number): Observable<void> {
    return this.ingredientClient.deleteIngredient(id);
  }
}