import { Injectable } from '@angular/core';
import { ReviewClient } from '../clients/review.client';
import { Observable } from 'rxjs';
import { Review, ReviewRequest } from '../models/review.model';
import { PaginationSettings } from '../models/pagination-settings.model';
import { PaginatedResult } from '../models/pagination-settings.model';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  constructor(private reviewClient: ReviewClient) {}

  public getAllReviews(paginationSettings: PaginationSettings): Observable<PaginatedResult<Review>> {
    return this.reviewClient.getAllReviews(paginationSettings);
  }

  public getReviewById(id: string): Observable<Review> {
    return this.reviewClient.getReviewById(id);
  }

  public getReviewsByRecipeId(recipeId: number, paginationSettings: PaginationSettings): Observable<PaginatedResult<Review>> {
    return this.reviewClient.getReviewsByRecipeId(recipeId, paginationSettings);
  }

  public createReview(reviewRequest: ReviewRequest): Observable<Review> {
    return this.reviewClient.createReview(reviewRequest);
  }

  public updateReview(id: string, reviewRequest: ReviewRequest): Observable<void> {
    return this.reviewClient.updateReview(id, reviewRequest);
  }

  public deleteReview(id: string): Observable<void> {
    return this.reviewClient.deleteReview(id);
  }
} 