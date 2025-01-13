import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorHandlingService } from '../services/error.service';
import { environment } from '../environments/environment';
import { Recipe } from '../models/recipe.model';
import { PaginationSettings } from '../models/pagination-settings.model';
import { CreateRecipeCommand} from '../models/create-recipe-command.model';
import { UpdateRecipeCommand } from '../models/update-recipe-command.model';
import { PaginatedResult } from '../models/pagination-settings.model';

@Injectable({
    providedIn: 'root',
  })
  export class RecipeClient {
    private apiUrl = environment.apiUrl;
  
    constructor(
      private http: HttpClient,
      private errorHandlingService: ErrorHandlingService
    ) {}
  
    public getAllRecipes(paginationSettings: PaginationSettings): Observable<PaginatedResult<Recipe>> {
      const params = new HttpParams()
        .set('pageNumber', paginationSettings.pageNumber.toString())
        .set('pageSize', paginationSettings.pageSize.toString());
  
      return this.http.get<PaginatedResult<Recipe>>(`${this.apiUrl}/api/recipes`, { params });
    }
  
    public getRecipesByUserName(userName: string, paginationSettings: PaginationSettings): Observable<PaginatedResult<Recipe>> {
      const params = new HttpParams()
        .set('pageNumber', paginationSettings.pageNumber.toString())
        .set('pageSize', paginationSettings.pageSize.toString());
  
      return this.http.get<PaginatedResult<Recipe>>(`${this.apiUrl}/api/recipes/userName/${userName}`, { params });
    }
  
    public getRecipeById(id: number): Observable<Recipe> {
      return this.http.get<Recipe>(`${this.apiUrl}/api/recipes/${id}`).pipe(
        catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
      );
    }
  
    public createRecipe(command: CreateRecipeCommand): Observable<Recipe> {
      return this.http.post<Recipe>(`${this.apiUrl}/api/recipes`, command).pipe(
        catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
      );
    }
  
    public updateRecipe(command: UpdateRecipeCommand): Observable<Recipe> {
      return this.http.put<Recipe>(`${this.apiUrl}/api/recipes`, command).pipe(
        catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
      );
    }
  
    public deleteRecipe(id: number): Observable<void> {
      return this.http.delete<void>(`${this.apiUrl}/api/recipes/${id}`).pipe(
        catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
      );
    }
  }