import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';
import { AccountingItem } from '../types/AccountingItem';
import { DevConfig } from '../configuration';

@Injectable({
  providedIn: 'root'
})
export class CostsService {

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
  ) { }

  getCosts() {
    return this.http.get(
      `${DevConfig.BASE_URI}/cost`,
      { headers: this.tokenService.getAuthHeaders() }
    );
  }

  getConcreteCost(id) {
    return this.http.get(
      `${DevConfig.BASE_URI}/cost/${id}`,
      { headers: this.tokenService.getAuthHeaders() }
      )
  }

  deleteCost(id: number) {
    return this.http.request('delete', `${DevConfig.BASE_URI}/cost`, {
      headers: this.tokenService.getAuthHeaders(),
      body: { ids:[id] } } );
  }

  addCost(cost: AccountingItem) {
    return this.http.request('post', `${DevConfig.BASE_URI}/cost`, {
      headers: this.tokenService.getAuthHeaders(),
      body: {
        category: cost.category,
        description: cost.description,
        price: cost.price,
        date: this.formatDate(cost.date)
      }
    });
  }

  editCost(cost: AccountingItem) {
    return this.http.request('put', `${DevConfig.BASE_URI}/cost/${cost.id}`, {
      headers: this.tokenService.getAuthHeaders(),
      body: {
        id: cost.id,
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
