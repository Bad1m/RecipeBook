import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Review, ReviewRequest } from '../models/review.model';
import { PaginationSettings } from '../models/pagination-settings.model';
import { PaginatedResult } from '../models/pagination-settings.model';

@Injectable({
  providedIn: 'root',
})
export class ReviewClient {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
  ) {}

  public getAllReviews(paginationSettings: PaginationSettings): Observable<PaginatedResult<Review>> {
    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());
  
    return this.http.get<PaginatedResult<Review>>(`${this.apiUrl}/api/reviews`, { params });
  }
  
  public getReviewsByRecipeId(recipeId: number, paginationSettings: PaginationSettings): Observable<PaginatedResult<Review>> {
    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());
  
    return this.http.get<PaginatedResult<Review>>(`${this.apiUrl}/api/reviews/recipe/${recipeId}`, { params });
  }

  public getReviewById(id: string): Observable<Review> {
    return this.http.get<Review>(`${this.apiUrl}/api/reviews/${id}`).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => error))
    );
  }

  public createReview(reviewRequest: ReviewRequest): Observable<Review> {
    return this.http.post<Review>(`${this.apiUrl}/api/reviews`, reviewRequest).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => error))
    );
  }

  public updateReview(id: string, reviewRequest: ReviewRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/api/reviews/${id}`, reviewRequest).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => error))
    );
  }

  public deleteReview(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/reviews/${id}`).pipe(
      catchError((error: HttpErrorResponse) => throwError(() => error))
    );
  }
}