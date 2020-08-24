import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccountingItem } from '../types/AccountingItem';
import { DevConfig } from '../configuration';
import { map, catchError } from 'rxjs/operators';
import { formatDateForTables, formatDate } from '../utils/formatDate';
import { aggregateCategories } from '../utils/aggregateCategories';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IncomesService {

  constructor(
    private http: HttpClient,
  ) { }

  getIncomes() {
    return this.http.get(
      `${DevConfig.BASE_URI}/income`,
    ).pipe(map(data => {
      return { formattedData: formatDateForTables(data), chartCosts: aggregateCategories(data) }
    }),
    catchError(err => {
      console.log(err);
      return throwError(err);
    }));
  }

  getConcreteIncome(id) {
    return this.http.get(`${DevConfig.BASE_URI}/income/${id}`)
  }

  deleteIncome(id: number) {
    return this.http.request('delete', `${DevConfig.BASE_URI}/income`, { body: { ids:[id] } } );
  }

  addIncome(income: AccountingItem) {
    return this.http.request('post', `${DevConfig.BASE_URI}/income`, {
      body: {
        category: income.category,
        description: income.description,
        price: income.price,
        date: formatDate(income.date)
      }
    });
  }

  editIncome(income: AccountingItem) {
    return this.http.request('put', `${DevConfig.BASE_URI}/income`, {
      body: {
        id: income.id,
        category: income.category,
        description: income.description,
        price: income.price,
        date: formatDate(income.date)
      }
    });
  }
}
