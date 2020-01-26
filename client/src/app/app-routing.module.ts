import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegistrationFormComponent } from './registration-form/registration-form.component'
import { HomeComponent } from './home/home.component';
import { AuthorizationFormComponent } from './authorization-form/authorization-form.component';


const routes: Routes = [
  { path: '', component:  HomeComponent },
  { path: 'registration', component: RegistrationFormComponent },
  { path: 'authorization', component: AuthorizationFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
