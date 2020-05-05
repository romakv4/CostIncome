import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { ValidateEqualValueDirective } from './directives/validate-equal-value.directive';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthPageComponent } from './auth-page/auth-page.component';
import { AuthorizationFormComponent } from './authorization-form/authorization-form.component';
import { ResetPasswordFormComponent } from './reset-password-form/reset-password-form.component';
import { CostsComponent } from './costs/costs.component';
import { ChangePasswordFormComponent } from './change-password-form/change-password-form.component';
import { AddCostFormComponent } from './add-cost-form/add-cost-form.component';
import { IncomesComponent } from './incomes/incomes.component';
import { AddIncomeFormComponent } from './add-income-form/add-income-form.component';
import { HomeComponent } from './home/home.component';
import { PieChartComponent } from './pie-chart/pie-chart.component';


@NgModule({
  declarations: [
    AppComponent,
    RegistrationFormComponent,
    ValidateEqualValueDirective,
    AuthPageComponent,
    AuthorizationFormComponent,
    ResetPasswordFormComponent,
    CostsComponent,
    ChangePasswordFormComponent,
    AddCostFormComponent,
    IncomesComponent,
    AddIncomeFormComponent,
    HomeComponent,
    PieChartComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    NgxPaginationModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
