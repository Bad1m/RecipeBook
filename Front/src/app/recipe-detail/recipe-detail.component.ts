import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RecipeService } from '../services/recipe.service';
import { Recipe } from '../models/recipe.model';
import { Ingredient } from '../models/ingredient.model';
import { Instruction } from '../models/instruction.model';
import { Review } from '../models/review.model';
import { IngredientService } from '../services/ingredient.service';
import { InstructionService } from '../services/instruction.service';
import { ReviewService } from '../services/review.service';
import { PaginatedResult } from '../models/pagination-settings.model';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { EditReviewModalComponent } from '../edit-review-modal/edit-review-modal.component';
import { ReviewRequest } from '../models/review.model';
import { ViewContainerRef } from '@angular/core';
import { BehaviorSubject, Observable, combineLatest, map } from 'rxjs';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss', '../styles/spinner.scss']
})
export class RecipeDetailComponent implements OnInit {
  recipe: Recipe | undefined;
  ingredients: Ingredient[] = [];
  instructions: Instruction[] = [];
  reviews: Review[] = [];
  reviewsCount: number = 0;
  paginationSettings = {
    pageNumber: 1,
    pageSize: 10
  };
  loading: boolean = false;
  newReview: { comment: string, rating: number } = { comment: '', rating: 1 };
  currentUser = typeof localStorage !== 'undefined' ? localStorage.getItem('userName') : null;
  recipe$ = new BehaviorSubject<Recipe | undefined>(undefined);
  reviews$ = new BehaviorSubject<Review[]>([]);
  isReviewsContainsRecipeUser$!: Observable<boolean>;

  constructor(
    private route: ActivatedRoute,
    protected ingredientService: IngredientService,
    protected instructionService: InstructionService,
    private reviewService: ReviewService,
    protected recipeService: RecipeService,
    protected dialog: MatDialog,
    private viewContainerRef: ViewContainerRef
  ) { }

  ngOnInit(): void {

    this.isReviewsContainsRecipeUser$ = combineLatest([this.recipe$, this.reviews$]).pipe(
      map(([recipe, reviews]) => {
        const isReviewsContainsRecipeUser = reviews.some(_ => _.userName === this.currentUser);
        return isReviewsContainsRecipeUser;
      })
    );
    this.route.paramMap.subscribe(params => {
      const recipeId = params.get('id');
      if (recipeId) {
        this.loading = true;
        this.loadRecipe(parseInt(recipeId, 10));
        this.loadIngredients(parseInt(recipeId, 10));
        this.loadInstructions(parseInt(recipeId, 10));
        this.loadReviews(parseInt(recipeId, 10));
      }
    });
  }

  onReviewsPageChange(event: PageEvent, recipeId: number): void {
    this.paginationSettings.pageNumber = event.pageIndex + 1;
    this.paginationSettings.pageSize = event.pageSize;
    this.loadReviews(recipeId);
  }

  protected loadRecipe(recipeId: number): void {
    this.recipeService.getRecipeById(recipeId).subscribe(
      (recipe) => {
        this.recipe = recipe;
        this.recipe$.next(recipe);
        this.loading = false;
      },
      () => {
        this.loading = false;
      }
    );
  }

  protected loadIngredients(recipeId: number): void {
    this.ingredientService.getIngredientsByRecipeId(recipeId).subscribe(
      (ingredients) => {
        this.ingredients = ingredients;
      }
    );
  }

  protected loadInstructions(recipeId: number): void {
    this.instructionService.getInstructionsByRecipeId(recipeId).subscribe(
      (instructions) => {
        this.instructions = instructions;
      }
    );
  }

  protected loadReviews(recipeId: number): void {
    this.reviewService.getReviewsByRecipeId(recipeId, this.paginationSettings).subscribe(
      (result: PaginatedResult<Review>) => {
        this.reviews = result.data;
        this.reviewsCount = result.totalCount;
        this.reviews$.next(result.data)
      }
    );
  }

  addReview(): void {
    const currentUser = localStorage.getItem('userName');
    if (currentUser && this.recipe) {
      const reviewRequest = {
        recipeId: this.recipe.id,
        userName: currentUser,
        comment: this.newReview.comment,
        rating: this.newReview.rating,
        date: new Date()
      };
  
      this.reviewService.createReview(reviewRequest).subscribe(
        (newReview) => {
          this.reviews.unshift(newReview);
          this.reviews$.next([...this.reviews]);
          this.newReview = { comment: '', rating: 1 };
        },
        (error) => {
          console.error('Error adding review:', error);
        }
      );
    }
  }

  editReview(review: Review): void {
    const dialogRef = this.dialog.open(EditReviewModalComponent, {
      width: '450px',
      data: { review: review },
      viewContainerRef: this.viewContainerRef, 
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const reviewRequest: ReviewRequest = {
          recipeId: review.recipeId,
          userName: review.userName, 
          date: review.date, 
          rating: result.rating,
          comment: result.comment,
        };
  
        review.comment = result.comment;
        review.rating = result.rating;
  
        this.reviewService.updateReview(review.id, reviewRequest).subscribe(
          () => {
          },
          (error) => {
            console.error('Error updating review:', error);
          }
        );
      }
    });
  }
}