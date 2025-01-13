import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInstructionModalComponent } from './add-instruction-modal.component';

describe('AddInstructionModalComponent', () => {
  let component: AddInstructionModalComponent;
  let fixture: ComponentFixture<AddInstructionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddInstructionModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddInstructionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
