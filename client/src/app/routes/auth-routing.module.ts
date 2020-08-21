import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthPageComponent } from '../components/auth-page/auth-page.component';
import { RegistrationFormComponent } from '../components/registration-form/registration-form.component';
import { AuthorizationFormComponent } from '../components/authorization-form/authorization-form.component';
import { ResetPasswordFormComponent } from '../components/reset-password-form/reset-password-form.component';
import { ChangePasswordFormComponent } from '../components/change-password-form/change-password-form.component';

const routes: Routes = [
  { path: '', component:  AuthPageComponent },
  { path: 'registration', component: RegistrationFormComponent },
  { path: 'authorization', component: AuthorizationFormComponent },
  { path: 'resetpassword', component: ResetPasswordFormComponent },
  { path: 'changepassword', component: ChangePasswordFormComponent },
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
