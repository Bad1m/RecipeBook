import { Component, OnInit, ViewChild } from '@angular/core';
import { Recipe } from '../models/recipe.model';
import { RecipeService } from '../services/recipe.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { PaginatedResult } from '../models/pagination-settings.model';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.scss', '../styles/spinner.scss']
})
export class RecipesComponent implements OnInit {
  recipes: Recipe[] = [];
  recipesCount: number = 0;
  paginationSettings = {
    pageNumber: 1,
    pageSize: 10
  };
  loading: boolean = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private recipeService: RecipeService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadRecipes();
  }

  onPageChange(event: PageEvent) {
    if (!this.loading) {
      this.paginationSettings = {
        pageNumber: event.pageIndex + 1,
        pageSize: event.pageSize
      }

      this.loadRecipes();
    }
  }

  private loadRecipes() {
    this.loading = true;
    this.recipeService.getAllRecipes(this.paginationSettings).subscribe(
      (result: PaginatedResult<Recipe>) => {
        this.recipes = result.data;
        this.recipesCount = result.totalCount;
        this.loading = false;
      },
      () => {
        this.loading = false;
      }
    );
  }

  viewDetails(recipeId: number) {
    console.log(recipeId);
    this.router.navigate(['/recipes', recipeId]);
  }
}