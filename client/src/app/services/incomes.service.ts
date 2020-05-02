import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';
import { AccountingItem } from '../types/AccountingItem';

@Injectable({
  providedIn: 'root'
})
export class IncomesService {
  
  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
  ) { }

  getIncomes() {
    return this.http.get('http://localhost:5000/api/v1/income', { headers: this.tokenService.getAuthHeaders() });
  }

  deleteIncome(id: number) {
    return this.http.request('delete', 'http://localhost:5000/api/v1/income', { 
      headers: this.tokenService.getAuthHeaders(),
      body: { ids:[id] } } );
  }

  addIncome(income: AccountingItem) {
    return this.http.request('post', 'http://localhost:5000/api/v1/income', {
      headers: this.tokenService.getAuthHeaders(),
      body: {
        category: income.category,
        description: income.description,
        price: income.price,
        date: this.formatDate(income.date)
      }
    });
  }

  private formatDate(date: Date) {
    const dateObj = new Date(date);
    return `${dateObj.getFullYear().toString()}-${(dateObj.getMonth() + 1).toString().padStart(2, '0')}-${dateObj.getDate().toString().padStart(2, '0')}`;
  }
}
