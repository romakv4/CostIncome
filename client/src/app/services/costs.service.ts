import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';
import { AccountingItem } from '../types/AccountingItem';

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

  addCost(cost: AccountingItem) {
    return this.http.request('post', 'http://localhost:5000/api/v1/cost', {
      headers: this.tokenService.getAuthHeaders(),
      body: {
        category: cost.category,
        description: cost.description,
        price: cost.price,
        date: this.formatDate(cost.date)
      }
    });
  }

  private formatDate(date: Date) {
    const dateObj = new Date(date);
    return `${dateObj.getFullYear().toString()}-${(dateObj.getMonth() + 1).toString().padStart(2, '0')}-${dateObj.getDate().toString().padStart(2, '0')}`;
  }
}
