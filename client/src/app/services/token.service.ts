import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  jwtHelper = new JwtHelperService();

  setToken(token) {
    sessionStorage.setItem('token', token);
  }

  getToken() {
    return sessionStorage.getItem('token');
  }

  getAuthHeaders() {
    return new HttpHeaders().set('Authorization', `Bearer ${this.getToken()}`);
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
