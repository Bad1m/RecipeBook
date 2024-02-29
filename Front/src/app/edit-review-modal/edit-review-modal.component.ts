import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Review } from '../models/review.model';

@Component({
  selector: 'app-edit-review-modal',
  templateUrl: './edit-review-modal.component.html',
  styleUrls: ['./edit-review-modal.component.scss', '../styles/modal.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EditReviewModalComponent {
  editedReview: Review;

  constructor(
    public dialogRef: MatDialogRef<EditReviewModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { review: Review }
  ) {
    this.editedReview = { ...data.review };
  }

  onSubmit(): void {
    this.dialogRef.close(this.editedReview);
  }
}