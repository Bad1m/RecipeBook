import { NgModule } from '@angular/core';
import { AuthGuard } from './helpers/auth.guard';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { RouterModule, Routes } from '@angular/router';
import { RecipesComponent } from './recipes/recipes.component';
import { PersonalAccountComponent } from './personal-account/personal-account.component';
import { HeaderComponent } from './header/header.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { RecipeDetailComponent } from './recipe-detail/recipe-detail.component';
import { RecipeUserDetailComponent } from './recipe-user-detail/recipe-user-detail.component';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    path: 'register',
    component: RegisterPageComponent,
  },
  {
    path: '',
    component: HeaderComponent,
    children: [
      { 
        path: 'recipes', 
        component: RecipesComponent,
        canActivate: [AuthGuard],
      },
      { 
        path: 'personal-account', 
        component: PersonalAccountComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'recipes/:id',
        component: RecipeDetailComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'user-recipes/:id',
        component: RecipeUserDetailComponent,
        canActivate: [AuthGuard],
      },
      { 
       path: 'add-recipe',
       component: AddRecipeComponent,
       canActivate: [AuthGuard],
      },
    ],
  },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }