import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CostsComponent } from '../components/costs/costs.component';
import { AddCostFormComponent } from '../components/add-cost-form/add-cost-form.component';
import { IncomesComponent } from '../components/incomes/incomes.component';
import { AddIncomeFormComponent } from '../components/add-income-form/add-income-form.component';
import { HomeComponent } from '../components/home/home.component';

const routes: Routes = [
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
