import { Injectable } from '@angular/core';
import { InstructionClient } from '../clients/instruction.client';
import { Observable } from 'rxjs';
import { Instruction } from '../models/instruction.model';
import { CreateInstructionForRecipeCommand, UpdateInstructionCommand } from '../models/instruction.model';

@Injectable({
  providedIn: 'root',
})
export class InstructionService {
  constructor(private instructionClient: InstructionClient) {}

  public getInstructionsByRecipeId(recipeId: number): Observable<Instruction[]> {
    return this.instructionClient.getInstructionsByRecipeId(recipeId);
  }

  public createInstruction(command: CreateInstructionForRecipeCommand): Observable<Instruction> {
    return this.instructionClient.createInstruction(command);
  }

  public updateInstruction(command: UpdateInstructionCommand): Observable<Instruction> {
    return this.instructionClient.updateInstruction(command);
  }

  public deleteInstruction(id: number): Observable<void> {
    return this.instructionClient.deleteInstruction(id);
  }
}