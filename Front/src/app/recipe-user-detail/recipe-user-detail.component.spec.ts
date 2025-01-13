import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeUserDetailComponent } from './recipe-user-detail.component';

describe('RecipeUserDetailComponent', () => {
  let component: RecipeUserDetailComponent;
  let fixture: ComponentFixture<RecipeUserDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RecipeUserDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RecipeUserDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
