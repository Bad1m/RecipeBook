import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateIngredientForRecipeCommand } from '../models/ingredient.model';

@Component({
  selector: 'app-add-ingredient-modal',
  templateUrl: './add-ingredient-modal.component.html',
  styleUrls: ['./add-ingredient-modal.component.scss', '../styles/modal.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class AddIngredientModalComponent {
  newIngredient: CreateIngredientForRecipeCommand = {
    name: '',
    amount: 0,
    unit: '',
    recipeId: 0,
  };

  constructor(
    public dialogRef: MatDialogRef<AddIngredientModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { recipeId: number }
  ) {
    this.newIngredient.recipeId = data.recipeId;
  }

  onSubmit(): void {
    this.dialogRef.close(this.newIngredient);
  }
}