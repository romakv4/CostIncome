import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {

  constructor(
    private router: Router,
  ) { }

  changePassword() {
    this.router.navigate(["changepassword"])
  }

}
