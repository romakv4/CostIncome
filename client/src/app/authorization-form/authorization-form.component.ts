import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-authorization-form',
  templateUrl: './authorization-form.component.html',
  styleUrls: ['./authorization-form.component.css']
})
export class AuthorizationFormComponent implements OnInit {

  authorizationForm;
  submitted = false;

  constructor(
    private formBulder: FormBuilder,
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
  }

}
