import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ResetPassUserData } from '../types/user';
import { Success } from '../types/authResponse';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reset-password-form',
  templateUrl: './reset-password-form.component.html',
  styleUrls: ['./reset-password-form.component.css']
})
export class ResetPasswordFormComponent {
  resetPasswordForm;
  serverErrors = null;
  secondsBeforeRedirect = 5;
  resetPasswordSuccess: boolean = null;
  submitted: boolean = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private formBulder: FormBuilder,
  ) { 
    this.resetPasswordForm = this.formBulder.group({
      email: ['', [Validators.required, Validators.email]]
    })
  }

  get f() { return this.resetPasswordForm.controls; }

  onSubmit(userData: ResetPassUserData) {
    this.submitted = true;
    if (this.resetPasswordForm.invalid) {
      return;
    }
    this.authService.resetPassword(userData)
      .subscribe(
        (response: Success) => {
          if (response.success) {
            this.resetPasswordSuccess = response.success;
            let interval = setInterval(() => {
              this.secondsBeforeRedirect--;
              if(this.secondsBeforeRedirect === 0) {
                clearInterval(interval);
              }
            }, 1000);
            setTimeout(() => {
              this.router.navigate(['authorization']);
            }, 5000);
          }
        },
        errorResponse => {
          this.serverErrors = errorResponse.error;
        }
      );
  }

  resetServerErrors() {
    if (this.serverErrors !== null) this.serverErrors = null;
  }

}
