import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class CostsService {

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
  ) { }

  getCosts() {
    return this.http.get('http://localhost:5000/api/v1/cost', { headers: this.tokenService.getAuthHeaders() });
  }

  deleteCost(id: number) {
    return this.http.request('delete', 'http://localhost:5000/api/v1/cost', { 
      headers: this.tokenService.getAuthHeaders(),
      body: { ids:[id] } } );
  }

  addCost(cost: any) {
    return this.http.post('http://localhost:5000/api/v1/cost', cost);
  }
}
