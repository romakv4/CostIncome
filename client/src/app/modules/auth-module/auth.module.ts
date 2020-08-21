import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RegistrationFormComponent } from 'src/app/components/registration-form/registration-form.component';
import { AuthPageComponent } from 'src/app/components/auth-page/auth-page.component';
import { AuthorizationFormComponent } from 'src/app/components/authorization-form/authorization-form.component';
import { ResetPasswordFormComponent } from 'src/app/components/reset-password-form/reset-password-form.component';
import { ChangePasswordFormComponent } from 'src/app/components/change-password-form/change-password-form.component';
import { BrowserModule } from '@angular/platform-browser';
import { ValidateEqualValueDirective } from 'src/app/directives/validate-equal-value.directive';

@NgModule({
  declarations: [
    ValidateEqualValueDirective,
    RegistrationFormComponent,
    AuthPageComponent,
    AuthorizationFormComponent,
    ResetPasswordFormComponent,
    ChangePasswordFormComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ]
})
export class AuthModule { }
