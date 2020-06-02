import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { ErrorsService } from '../services/errors.service';
import { ChangePasswordUserData } from '../types/user';
import { Success } from '../types/authResponse';

@Component({
  selector: 'app-change-password-form',
  templateUrl: './change-password-form.component.html',
  styleUrls: ['./change-password-form.component.css']
})
export class ChangePasswordFormComponent {

  changePasswordForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  changePasswordSuccess: boolean = null;
  submitted: boolean = false;

  constructor(
    private formBulder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private location: Location,
    private errorsService: ErrorsService,
  ) { 
    this.changePasswordForm = this.formBulder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
    })
  }

  get f() { return this.changePasswordForm.controls; }

  onSubmit(userData: ChangePasswordUserData) {
    this.submitted = true;
    if (this.changePasswordForm.invalid) {
      return;
    }
    this.authService.changePassword(userData)
      .subscribe(
        (response: Success) => {
          this.changePasswordSuccess = response.success;
          if (this.changePasswordSuccess) {
            this.authService.logout();
            this.router.navigate(['authorization']);
          }
        },
        errorResponse => { 
          this.serverErrors = errorResponse.error
        }
      );
  }

  back() {
    this.location.back()
  }

}
