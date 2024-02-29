import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RecipeService } from '../services/recipe.service';
import { UpdateRecipeCommand } from '../models/update-recipe-command.model';

@Component({
  selector: 'app-edit-recipe-modal',
  templateUrl: './edit-recipe-modal.component.html',
  styleUrls: ['./edit-recipe-modal.component.scss', '../styles/modal.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EditRecipeModalComponent {
  editedRecipe: UpdateRecipeCommand;
  imageError: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<EditRecipeModalComponent>,
    private recipeService: RecipeService,
    @Inject(MAT_DIALOG_DATA) public data: { recipe: UpdateRecipeCommand }
  ) {
    this.editedRecipe = { ...data.recipe };
  }

  handleImageError() {
    this.imageError = true;
  }

  clearImageError() {
    this.imageError = false;
  }

  onSubmit(): void {
  this.editedRecipe.user = this.data.recipe.user;
    this.recipeService.updateRecipe(this.editedRecipe).subscribe(updatedRecipe => {
      this.dialogRef.close(updatedRecipe);
    });
  }
}