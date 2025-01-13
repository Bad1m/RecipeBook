import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Instruction } from '../models/instruction.model';

@Component({
  selector: 'app-edit-instruction-modal',
  templateUrl: './edit-instruction-modal.component.html',
  styleUrls: ['./edit-instruction-modal.component.scss', '../styles/modal.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EditInstructionModalComponent {
  editedInstruction: Instruction;

  constructor(
    public dialogRef: MatDialogRef<EditInstructionModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { instruction: Instruction }
  ) {
    this.editedInstruction = { ...data.instruction };
  }

  onSubmit(): void {
    this.dialogRef.close(this.editedInstruction); 
  }
}