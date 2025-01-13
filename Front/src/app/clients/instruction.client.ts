import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorHandlingService } from '../services/error.service';
import { environment } from '../environments/environment';
import { Instruction } from '../models/instruction.model';
import { CreateInstructionForRecipeCommand, UpdateInstructionCommand } from '../models/instruction.model';

@Injectable({
  providedIn: 'root',
})
export class InstructionClient {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private errorHandlingService: ErrorHandlingService
  ) {}

  public getInstructionsByRecipeId(recipeId: number): Observable<Instruction[]> {
    return this.http.get<Instruction[]>(`${this.apiUrl}/api/instructions/${recipeId}`).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }

  public createInstruction(command: CreateInstructionForRecipeCommand): Observable<Instruction> {
    return this.http.post<Instruction>(`${this.apiUrl}/api/instructions`, command).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }

  public updateInstruction(command: UpdateInstructionCommand): Observable<Instruction> {
    return this.http.put<Instruction>(`${this.apiUrl}/api/instructions`, command).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }

  public deleteInstruction(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/instructions/${id}`).pipe(
      catchError((error: HttpErrorResponse) => this.errorHandlingService.handleError(error))
    );
  }
}