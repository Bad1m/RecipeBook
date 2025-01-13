import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-recipe-modal',
  templateUrl: './delete-recipe-modal.component.html',
  styleUrls: ['./delete-recipe-modal.component.scss', '../styles/modal.scss'],
})
export class DeleteRecipeModalComponent {
  constructor(public dialogRef: MatDialogRef<DeleteRecipeModalComponent>) {}

  confirmDelete(): void {
    this.dialogRef.close(true);
  }

  cancelDelete(): void {
    this.dialogRef.close(false);
  }
}