import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditIngredientModalComponent } from './edit-ingredient-modal.component';

describe('EditIngredientModalComponent', () => {
  let component: EditIngredientModalComponent;
  let fixture: ComponentFixture<EditIngredientModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditIngredientModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditIngredientModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
