import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorHandlingService } from '../services/error.service';
import { Ingredient } from '../models/ingredient.model';
import { CreateIngredientForRecipeCommand, UpdateIngredientCommand } from '../models/ingredient.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class IngredientClient {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private errorHandlingService: ErrorHandlingService
  ) {}

  public getIngredientsByRecipeId(recipeId: number): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(`${this.apiUrl}/api/ingredients/${recipeId}`).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }

  public createIngredient(command: CreateIngredientForRecipeCommand): Observable<Ingredient> {
    return this.http.post<Ingredient>(`${this.apiUrl}/api/ingredients`, command).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }

  public updateIngredient(command: UpdateIngredientCommand): Observable<Ingredient> {
    return this.http.put<Ingredient>(`${this.apiUrl}/api/ingredients`, command).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }

  public deleteIngredient(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/ingredients/${id}`).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }
}