import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ResetPassUserData } from '../../types/user';
import { Success } from '../../types/authResponse';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RedirectService } from '../../services/redirect.service';
import { ErrorsService } from '../../services/errors.service';

@Component({
  selector: 'app-reset-password-form',
  templateUrl: './reset-password-form.component.html',
  styleUrls: ['./reset-password-form.component.css']
})
export class ResetPasswordFormComponent {
  resetPasswordForm;
  serverErrors = null;
  resetServerErrors = this.errorsService.resetServerErrors;
  secondsBeforeRedirect = 5;
  resetPasswordSuccess: boolean = null;
  submitted = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private formBulder: FormBuilder,
    private errorsService: ErrorsService,
    private redirects: RedirectService,
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
            this.redirects.delayCounter(this);
            this.redirects.delayedRedirect(5000, 'authorization', this.router);
          }
        },
        errorResponse => {
          this.serverErrors = errorResponse.error;
        }
      );
  }
}
