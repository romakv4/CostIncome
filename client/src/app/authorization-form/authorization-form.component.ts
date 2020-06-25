import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Success } from '../types/authResponse';
import { SignInUserData } from '../types/user';
import { ErrorsService } from '../services/errors.service';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-authorization-form',
  templateUrl: './authorization-form.component.html',
  styleUrls: ['./authorization-form.component.css']
})
export class AuthorizationFormComponent implements OnInit {

  authorizationForm;
  serverErrors; 
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted: boolean = false;
  authorizationSuccess: boolean;

  constructor(
    private formBulder: FormBuilder,
    private authService: AuthService,
    private errorsService: ErrorsService,
    private tokenService: TokenService,
    private router: Router,
  ) {
    this.authorizationForm = this.formBulder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    })
  }

  ngOnInit(): void {
    if (this.tokenService.getToken() != null && !this.tokenService.isTokenExpired()) {
      this.router.navigate(['home']);
    }
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
            this.tokenService.setToken(response.token);
            this.router.navigate(['home']);
          }
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      );
  }
}
