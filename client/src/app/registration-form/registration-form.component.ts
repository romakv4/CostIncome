import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { SignUpUserData } from '../types/user';
import { Success } from "../types/authResponse";
import { Router } from '@angular/router';
import { ErrorsService } from '../services/errors.service';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent {

  registrationForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  registrationSuccess: boolean = null;
  submitted: boolean = false;

  constructor(
    private formBulder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private errorsService: ErrorsService,
  ) { 
    this.registrationForm = this.formBulder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required],
    })
  }

  get f() { return this.registrationForm.controls; }

  onSubmit(userData: SignUpUserData) {
    this.submitted = true;
    if (this.registrationForm.invalid) {
      return;
    }
    this.authService.register(userData)
      .subscribe(
        (response: Success) => {
          this.registrationSuccess = response.success;
          if (this.registrationSuccess) {
            this.router.navigate(['authorization']);
          }
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      );
  }
}
