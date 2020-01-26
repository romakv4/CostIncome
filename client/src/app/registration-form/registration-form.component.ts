import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {

  registrationForm;
  submitted = false;

  constructor(
    private formBulder: FormBuilder,
  ) { 
    this.registrationForm = this.formBulder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      repeatPassword: ['', Validators.required],
    })
  }

  ngOnInit() {
  }

  get f() { return this.registrationForm.controls; }

  onSubmit(userData: any) {
    this.submitted = true;
  }

}
