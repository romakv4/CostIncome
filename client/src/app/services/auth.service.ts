import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { 
  SignInUserData,
  SignUpUserData,
  ResetPassUserData,
  ChangePasswordUserData
} from '../types/user'
import { DevConfig } from '../configuration';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
  ) { }

  register(userData: SignUpUserData) {
    return this.http.post(`${DevConfig.AUTH_URI}/register`, userData);
  }

  authorize(userData: SignInUserData) {
    return this.http.post(`${DevConfig.AUTH_URI}/login`, userData);
  }

  resetPassword(userData: ResetPassUserData) {
    return this.http.post(`${DevConfig.AUTH_URI}/resetpassword`, userData);
  }

  changePassword(userData: ChangePasswordUserData) {
    return this.http.post(`${DevConfig.AUTH_URI}/changepassword`, userData);
  }
}
