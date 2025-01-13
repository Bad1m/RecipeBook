import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { TokenInterceptor } from './helpers/token.interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { FlexLayoutServerModule } from '@angular/flex-layout/server';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './header/header.component';
import { RecipesComponent } from './recipes/recipes.component';
import { PersonalAccountComponent } from './personal-account/personal-account.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NotFoundComponent } from './not-found/not-found.component';
import { RecipeDetailComponent } from './recipe-detail/recipe-detail.component';
import { RecipeUserDetailComponent } from './recipe-user-detail/recipe-user-detail.component';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EditRecipeModalComponent } from './edit-recipe/edit-recipe-modal.component';
import { EditReviewModalComponent } from './edit-review-modal/edit-review-modal.component';
import { DeleteRecipeModalComponent } from './delete-recipe-modal/delete-recipe-modal.component';
import { MatDialogModule } from '@angular/material/dialog';
import { AddInstructionModalComponent } from './add-instruction-modal/add-instruction-modal.component';
import { AddIngredientModalComponent } from './add-ingredient-modal/add-ingredient-modal.component';
import { EditInstructionModalComponent } from './edit-instruction-modal/edit-instruction-modal.component';
import { EditIngredientModalComponent } from './edit-ingredient-modal/edit-ingredient-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterPageComponent,
    LoginPageComponent,
    HeaderComponent,
    RecipesComponent,
    PersonalAccountComponent,
    NotFoundComponent,
    RecipeDetailComponent,
    RecipeUserDetailComponent,
    AddRecipeComponent,
    EditRecipeModalComponent,
    EditReviewModalComponent,
    DeleteRecipeModalComponent,
    AddInstructionModalComponent,
    AddIngredientModalComponent,
    EditInstructionModalComponent,
    EditIngredientModalComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatButtonModule,
    MatFormFieldModule,
    MatCardModule,
    MatInputModule,
    HttpClientModule,
    FlexLayoutServerModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatDialogModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    provideHttpClient(withFetch()), 
    provideClientHydration(),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }