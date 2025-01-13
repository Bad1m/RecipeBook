import { Component, OnInit, ViewChild } from '@angular/core';
import { Recipe } from '../models/recipe.model';
import { RecipeService } from '../services/recipe.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { PaginatedResult } from '../models/pagination-settings.model';
import { DeleteRecipeModalComponent } from '../delete-recipe-modal/delete-recipe-modal.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-personal-account',
  templateUrl: './personal-account.component.html',
  styleUrls: ['../recipes/recipes.component.scss', './personal-account.component.scss', '../styles/spinner.scss']
})
export class PersonalAccountComponent implements OnInit {
  userRecipes: Recipe[] = [];
  userRecipesCount: number = 0;
  paginationSettings = {
    pageNumber: 1,
    pageSize: 10
  };
  loading: boolean = false; 

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private recipeService: RecipeService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadUserRecipes();
  }

  onPageChange(event: PageEvent) {
    if (!this.loading) {
      this.paginationSettings.pageNumber = event.pageIndex + 1;
      this.paginationSettings.pageSize = event.pageSize;
      this.loadUserRecipes();
    }
  }

  private loadUserRecipes() {
    this.loading = true; 
    const userName = typeof localStorage !== 'undefined' ? localStorage.getItem('userName') : null;
    if(userName){
      this.recipeService.getRecipesByUserName(userName, this.paginationSettings).subscribe(
        (result: PaginatedResult<Recipe>) => {
          this.userRecipes = result.data; 
          this.userRecipesCount = result.totalCount;
          this.loading = false; 
        },
        () => {
          this.loading = false; 
        }
      );
    }
  }

  viewDetails(recipeId: number) {
    this.router.navigate(['/user-recipes', recipeId]);
  }

  deleteRecipe(recipeId: number) {
    const dialogRef = this.dialog.open(DeleteRecipeModalComponent);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.recipeService.deleteRecipe(recipeId).subscribe(() => {
          this.loadUserRecipes();
        });
      }
    });
  }

  navigateToAddRecipe() {
    this.router.navigate(['/add-recipe']);
  }
}