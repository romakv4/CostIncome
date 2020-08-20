import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegistrationFormComponent } from './registration-form/registration-form.component'
import { AuthPageComponent } from './auth-page/auth-page.component';
import { AuthorizationFormComponent } from './authorization-form/authorization-form.component';
import { ResetPasswordFormComponent } from './reset-password-form/reset-password-form.component';
import { CostsComponent } from './costs/costs.component';
import { ChangePasswordFormComponent } from './change-password-form/change-password-form.component';
import { AddCostFormComponent } from './add-cost-form/add-cost-form.component';
import { IncomesComponent } from './incomes/incomes.component';
import { AddIncomeFormComponent } from './add-income-form/add-income-form.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '', component:  AuthPageComponent },
  { path: 'registration', component: RegistrationFormComponent },
  { path: 'authorization', component: AuthorizationFormComponent },
  { path: 'resetpassword', component: ResetPasswordFormComponent },
  { path: 'changepassword', component: ChangePasswordFormComponent },
  { path: 'home', component: HomeComponent },
  { path: 'costs', component: CostsComponent },
  { path: 'add-cost', component: AddCostFormComponent },
  { path: 'incomes', component: IncomesComponent },
  { path: 'add-income', component: AddIncomeFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
