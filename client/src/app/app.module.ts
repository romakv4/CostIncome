import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './routes/app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CostsComponent } from './components/costs/costs.component';
import { AddCostFormComponent } from './components/add-cost-form/add-cost-form.component';
import { IncomesComponent } from './components/incomes/incomes.component';
import { AddIncomeFormComponent } from './components/add-income-form/add-income-form.component';
import { HomeComponent } from './components/home/home.component';
import { PieChartComponent } from './components/pie-chart/pie-chart.component';
import { AccountingItemsTableComponent } from './components/accounting-items-table/accounting-items-table.component';
import { EditCostFormComponent } from './components/edit-cost-form/edit-cost-form.component';
import { EditIncomeFormComponent } from './components/edit-income-form/edit-income-form.component';
import { GeneralActionsBarComponent } from './components/general-actions-bar/general-actions-bar.component';
import { AuthRoutingModule } from './routes/auth-routing.module';
import { AuthModule } from './modules/auth/auth.module';
import { TokenInterceptor } from './interceptors/token.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    CostsComponent,
    AddCostFormComponent,
    IncomesComponent,
    AddIncomeFormComponent,
    HomeComponent,
    PieChartComponent,
    AccountingItemsTableComponent,
    EditCostFormComponent,
    EditIncomeFormComponent,
    GeneralActionsBarComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    AuthRoutingModule,
    AuthModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    NgxPaginationModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
