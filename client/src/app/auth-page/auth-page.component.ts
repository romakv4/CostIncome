import { Component, OnInit } from '@angular/core';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.css']
})
export class AuthPageComponent implements OnInit {

  constructor(
    private tokenService: TokenService,
    private router: Router,
  ) { }

  ngOnInit() {
    if (this.tokenService.getToken() != null && !this.tokenService.isTokenExpired()) {
      this.router.navigate(['home']);
    }
  }
}
