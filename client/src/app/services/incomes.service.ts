import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';
import { AccountingItem } from '../types/AccountingItem';
import { DevConfig } from '../configuration';
import { map, catchError } from 'rxjs/operators';
import { formatDateForTables } from '../utils/formatDate';
import { aggregateCategories } from '../utils/aggregateCategories';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IncomesService {

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
  ) { }

  getIncomes() {
    return this.http.get(
      `${DevConfig.BASE_URI}/income`,
      { headers: this.tokenService.getAuthHeaders() }
    ).pipe(map(data => {
      return { formattedData: formatDateForTables(data), chartCosts: aggregateCategories(data) }
    }),
    catchError(err => {
      console.log(err);
      return throwError(err);
    }));
  }

  getConcreteIncome(id) {
    return this.http.get(
      `${DevConfig.BASE_URI}/income/${id}`,
      { headers: this.tokenService.getAuthHeaders() }
    )
  }

  deleteIncome(id: number) {
    return this.http.request('delete', `${DevConfig.BASE_URI}/income`, {
      headers: this.tokenService.getAuthHeaders(),
      body: { ids:[id] } } );
  }

  addIncome(income: AccountingItem) {
    return this.http.request('post', `${DevConfig.BASE_URI}/income`, {
      headers: this.tokenService.getAuthHeaders(),
      body: {
        category: income.category,
        description: income.description,
        price: income.price,
        date: this.formatDate(income.date)
      }
    });
  }

  editIncome(income: AccountingItem) {
    return this.http.request('put', `${DevConfig.BASE_URI}/income`, {
      headers: this.tokenService.getAuthHeaders(),
      body: {
        id: income.id,
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
