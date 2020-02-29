import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { 
  SignInUserData,
  SignUpUserData,
  ResetPassUserData
} from '../types/user'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
  ) { }

  register(userData: SignUpUserData) {
    return this.http.post('http://localhost:5000/api/auth/register', userData);
  }

  authorize(userData: SignInUserData) {
    return this.http.post('http://localhost:5000/api/auth/login', userData);
  }

  resetPassword(userData: ResetPassUserData) {
    return this.http.post('http://localhost:5000/api/auth/resetpassword', userData);
  }
}
