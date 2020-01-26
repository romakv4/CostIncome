import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { ValidateEqualValueDirective } from './directives/validate-equal-value.directive';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { AuthorizationFormComponent } from './authorization-form/authorization-form.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationFormComponent,
    ValidateEqualValueDirective,
    HomeComponent,
    AuthorizationFormComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
