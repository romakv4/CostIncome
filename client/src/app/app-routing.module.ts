import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegistrationFormComponent } from './registration-form/registration-form.component'
import { AuthPageComponent } from './auth-page/auth-page.component';
import { AuthorizationFormComponent } from './authorization-form/authorization-form.component';
import { ResetPasswordFormComponent } from './reset-password-form/reset-password-form.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '', component:  AuthPageComponent },
  { path: 'registration', component: RegistrationFormComponent },
  { path: 'authorization', component: AuthorizationFormComponent },
  { path: 'resetpassword', component: ResetPasswordFormComponent },
  { path: 'home', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
