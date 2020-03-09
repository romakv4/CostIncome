import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { ValidateEqualValueDirective } from './directives/validate-equal-value.directive';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthPageComponent } from './auth-page/auth-page.component';
import { AuthorizationFormComponent } from './authorization-form/authorization-form.component';
import { ResetPasswordFormComponent } from './reset-password-form/reset-password-form.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationFormComponent,
    ValidateEqualValueDirective,
    AuthPageComponent,
    AuthorizationFormComponent,
    ResetPasswordFormComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
