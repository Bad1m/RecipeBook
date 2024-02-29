import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateInstructionForRecipeCommand } from '../models/instruction.model';

@Component({
  selector: 'app-add-instruction-modal',
  templateUrl: './add-instruction-modal.component.html',
  styleUrls: ['./add-instruction-modal.component.scss', '../styles/modal.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class AddInstructionModalComponent {
  newInstruction: CreateInstructionForRecipeCommand = {
    stepNumber: 0,
    description: '',
    recipeId: 0,
  };

  constructor(
    public dialogRef: MatDialogRef<AddInstructionModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { recipeId: number }
  ) {
    this.newInstruction.recipeId = data.recipeId;
  }

  onSubmit(): void {
    this.dialogRef.close(this.newInstruction);
  }
}