import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Success } from '../types/authResponse';
import { SignInUserData } from '../types/user';
import { ErrorsService } from '../services/errors.service';

@Component({
  selector: 'app-authorization-form',
  templateUrl: './authorization-form.component.html',
  styleUrls: ['./authorization-form.component.css']
})
export class AuthorizationFormComponent {

  authorizationForm;
  serverErrors; 
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted: boolean = false;
  authorizationSuccess: boolean;

  constructor(
    private formBulder: FormBuilder,
    private authService: AuthService,
    private errorsService: ErrorsService,
  ) {
    this.authorizationForm = this.formBulder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    })
  }

  get f() { return this.authorizationForm.controls; }

  onSubmit(userData: SignInUserData) {
    this.submitted = true;
    if (this.authorizationForm.invalid) {
      return;
    }
    this.authService.authorize(userData)
      .subscribe(
        (response: Success) => {
          this.authorizationSuccess = response.success;
          if (this.authorizationSuccess) {
            sessionStorage.setItem("token", response.token);
          }
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      );
  }
}
