import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteRecipeModalComponent } from './delete-recipe-modal.component';

describe('DeleteRecipeModalComponent', () => {
  let component: DeleteRecipeModalComponent;
  let fixture: ComponentFixture<DeleteRecipeModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeleteRecipeModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeleteRecipeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
