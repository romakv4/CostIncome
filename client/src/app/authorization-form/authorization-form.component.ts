import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Success } from '../types/authResponse';

@Component({
  selector: 'app-authorization-form',
  templateUrl: './authorization-form.component.html',
  styleUrls: ['./authorization-form.component.css']
})
export class AuthorizationFormComponent implements OnInit {

  authorizationForm;
  serverErrors;
  submitted: boolean = false;
  authorizationSuccess: boolean;

  constructor(
    private formBulder: FormBuilder,
    private authService: AuthService,
  ) {
    this.authorizationForm = this.formBulder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    })
  }

  ngOnInit() {
  }

  get f() { return this.authorizationForm.controls; }

  onSubmit(userData: any) {
    this.submitted = true;
    if (this.authorizationForm.invalid) {
      return;
    }
    this.authService.authorize(userData)
      .subscribe(
        (response: Success) => {
          this.authorizationSuccess = response.success;
          if (this.authorizationSuccess) {
            localStorage.setItem("token", response.token);
          }
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      );
  }

}
