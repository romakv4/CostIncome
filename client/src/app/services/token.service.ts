import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  jwtHelper = new JwtHelperService();

  constructor() { }

  setToken(token) {
    sessionStorage.setItem('token', token);
  }

  getToken() {
    return sessionStorage.getItem('token');
  }

  logout(router) {
    sessionStorage.removeItem('token');
    router.navigate(['authorization']);
  }

  isLoggedIn() {
    const token = sessionStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
