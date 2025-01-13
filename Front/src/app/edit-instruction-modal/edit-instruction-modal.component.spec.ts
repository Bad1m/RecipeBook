import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditInstructionModalComponent } from './edit-instruction-modal.component';

describe('EditInstructionModalComponent', () => {
  let component: EditInstructionModalComponent;
  let fixture: ComponentFixture<EditInstructionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditInstructionModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditInstructionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
