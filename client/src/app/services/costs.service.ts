import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccountingItem } from '../types/AccountingItem';
import { DevConfig } from '../configuration';
import { throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { formatDateForTables, formatDate } from '../utils/formatDate';
import { aggregateCategories } from '../utils/aggregateCategories';

@Injectable({
  providedIn: 'root'
})
export class CostsService {

  constructor(
    private http: HttpClient,
  ) { }

  getCosts() {
    return this.http.get(
      `${DevConfig.BASE_URI}/cost`,
    ).pipe(map(data => {
      return { formattedData: formatDateForTables(data), chartCosts: aggregateCategories(data) }
    }),
    catchError(err => {
      console.log(err);
      return throwError(err);
    }));
  }

  getConcreteCost(id) {
    return this.http.get(`${DevConfig.BASE_URI}/cost/${id}`)
  }

  deleteCost(id: number) {
    return this.http.request('delete', `${DevConfig.BASE_URI}/cost`, { body: { ids:[id] } } );
  }

  addCost(cost: AccountingItem) {
    return this.http.request('post', `${DevConfig.BASE_URI}/cost`, {
      body: {
        category: cost.category,
        description: cost.description,
        price: cost.price,
        date: formatDate(cost.date)
      }
    });
  }

  editCost(cost: AccountingItem) {
    return this.http.request('put', `${DevConfig.BASE_URI}/cost`, {
      body: {
        id: cost.id,
        category: cost.category,
        description: cost.description,
        price: cost.price,
        date: formatDate(cost.date)
      }
    });
  }
}
