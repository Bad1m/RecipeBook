import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UpdateIngredientCommand } from '../models/ingredient.model';

@Component({
  selector: 'app-edit-ingredient-modal',
  templateUrl: './edit-ingredient-modal.component.html',
  styleUrls: ['./edit-ingredient-modal.component.scss', '../styles/modal.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EditIngredientModalComponent {
  editedIngredient: UpdateIngredientCommand;

  constructor(
    public dialogRef: MatDialogRef<EditIngredientModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { ingredient: UpdateIngredientCommand }
  ) {
    this.editedIngredient = { ...data.ingredient };
  }

  onSubmit(): void {
    this.dialogRef.close(this.editedIngredient);
  }
}