import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IngredientService } from '../services/ingredient.service';
import { InstructionService } from '../services/instruction.service';
import { ReviewService } from '../services/review.service';
import { RecipeService } from '../services/recipe.service';
import { RecipeDetailComponent } from '../recipe-detail/recipe-detail.component';
import { MatDialog } from '@angular/material/dialog';
import { EditRecipeModalComponent } from '../edit-recipe/edit-recipe-modal.component';
import { EditIngredientModalComponent } from '../edit-ingredient-modal/edit-ingredient-modal.component';
import { EditInstructionModalComponent } from '../edit-instruction-modal/edit-instruction-modal.component';
import { AddIngredientModalComponent } from '../add-ingredient-modal/add-ingredient-modal.component';
import { AddInstructionModalComponent } from '../add-instruction-modal/add-instruction-modal.component';
import { ViewContainerRef } from '@angular/core';
import { UpdateRecipeCommand } from '../models/update-recipe-command.model';
import { UpdateIngredientCommand } from '../models/ingredient.model';

@Component({
  selector: 'app-recipe-user-detail',
  templateUrl: './recipe-user-detail.component.html',
  styleUrls: ['../recipe-detail/recipe-detail.component.scss', './recipe-user-detail.component.scss', '../styles/spinner.scss', '../styles/buttons.scss']
})
export class RecipeUserDetailComponent extends RecipeDetailComponent { 


  constructor(
    route: ActivatedRoute,
    ingredientService: IngredientService,
    instructionService: InstructionService,
    reviewService: ReviewService,
    recipeService: RecipeService,
    dialog: MatDialog,
    viewContainerRef : ViewContainerRef,
  ) {
    super(route, ingredientService, instructionService, reviewService, recipeService, dialog, viewContainerRef);
  }

  openEditRecipeModal(): void {
    const dialogRef = this.dialog.open(EditRecipeModalComponent, {
      data: { recipe: this.recipe },
    });
  
    dialogRef.afterClosed().subscribe((updatedRecipe: UpdateRecipeCommand) => {
      if (updatedRecipe) {
        this.recipe = {
          id: updatedRecipe.id,
          dish: updatedRecipe.dish || this.recipe!.dish,
          category: updatedRecipe.category || this.recipe!.category,
          description: updatedRecipe.description || this.recipe!.description,
          prepTime: updatedRecipe.prepTime || this.recipe!.prepTime,
          img: updatedRecipe.img || this.recipe!.img,
          user: updatedRecipe.user || this.recipe!.user,
        };
      }
    });
  }

  openAddIngredientModal(): void {
    const dialogRef = this.dialog.open(AddIngredientModalComponent, {
      data: { recipeId: this.recipe!.id },
    });

    dialogRef.afterClosed().subscribe(newIngredient => {
      if (newIngredient) {
        this.ingredientService.createIngredient({
          recipeId: newIngredient.recipeId,
          name: newIngredient.name,
          amount: newIngredient.amount,
          unit: newIngredient.unit
        }).subscribe(
          (createdIngredient) => {
            this.loadRecipe(this.recipe!.id);
          },
          (error) => {
            console.error('Error adding ingredient:', error);
          }
        );
      }
    });
  }

  openEditIngredientModal(ingredientIndex: number): void {
    const selectedIngredient = this.recipe!.recipeIngredients![ingredientIndex];

    const dialogRef = this.dialog.open(EditIngredientModalComponent, {
      data: { ingredient: selectedIngredient }
    });

    dialogRef.afterClosed().subscribe(updatedIngredient => {
      if (updatedIngredient) {
        const updateCommand: UpdateIngredientCommand = {
          id: updatedIngredient.ingredient.id,
          name: updatedIngredient.ingredient.name,
          unit: updatedIngredient.ingredient.unit,
          amount: updatedIngredient.amount
        };

        this.ingredientService.updateIngredient(updateCommand).subscribe(
          (updatedIngredientResponse) => {
            this.loadRecipe(this.recipe!.id);
          },
          (error) => {
            console.error('Error updating ingredient:', error);
          }
        );
      }
    });
  }

  openAddInstructionModal(): void {
    const dialogRef = this.dialog.open(AddInstructionModalComponent, {
      data: { recipeId: this.recipe!.id },
    });
  
    dialogRef.afterClosed().subscribe(newInstruction => {
      if (newInstruction) {
        this.instructionService.createInstruction({
          recipeId: newInstruction.recipeId,
          stepNumber: newInstruction.stepNumber,
          description: newInstruction.description
        }).subscribe(
          (createdInstruction) => {
            this.loadInstructions(this.recipe!.id);
          },
          (error) => {
            console.error('Error adding instruction:', error);
          }
        );
      }
    });
  }

  openEditInstructionModal(instructionIndex: number): void {
    const selectedInstruction = this.instructions![instructionIndex];
    const dialogRef = this.dialog.open(EditInstructionModalComponent, {
      data: { instruction: selectedInstruction },
    });

    dialogRef.afterClosed().subscribe(editedInstruction => {
      if (editedInstruction) {
        this.instructionService.updateInstruction({
          id: editedInstruction.id,
          stepNumber: editedInstruction.stepNumber,
          description: editedInstruction.description
        }).subscribe(
          () => {
            this.loadInstructions(this.recipe!.id);
          },
          (error) => {
            console.error('Error updating instruction:', error);
          }
        );
      }
    });
  }

  deleteIngredient(ingredientIndex: number): void {
    const selectedIngredient = this.recipe!.recipeIngredients![ingredientIndex];
    this.ingredientService.deleteIngredient(selectedIngredient.ingredient.id).subscribe(
      () => {
        this.loadRecipe(this.recipe!.id);
      },
      (error) => {
        console.error('Error deleting ingredient:', error);
      }
    );
  }
  
  deleteInstruction(instructionIndex: number): void {
    const selectedInstruction = this.instructions![instructionIndex];
    this.instructionService.deleteInstruction(selectedInstruction.id).subscribe(
      () => {
        this.loadInstructions(this.recipe!.id);
      },
      (error) => {
        console.error('Error deleting instruction:', error);
      }
    );
  }
}