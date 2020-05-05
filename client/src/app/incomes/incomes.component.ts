import { Component, OnInit } from '@angular/core';
import { IncomesService } from '../services/incomes.service';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';
import { aggregateCategories } from '../utils/aggregateCategories';
import { AccountingItem } from '../types/AccountingItem';
import { formatDateForTables } from '../utils/formatDateForTables';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.css']
})
export class IncomesComponent implements OnInit {

  incomes = null;
  chartIncomes: Array<{}> = [];
  itemsPerPage = "14";
  currentPage = "1";

  constructor(
    private incomesService: IncomesService,
    private tokenService: TokenService,
    private router: Router,
  ) { }

  ngOnInit() {
    if (!this.tokenService.isLoggedIn()) {
      this.router.navigate(['authorization']);
    }
    this.incomesService.getIncomes()
      .subscribe(
        (data: Array<AccountingItem>) => {
          const formattedData = formatDateForTables(data);
          this.incomes = formattedData;
          this.chartIncomes = aggregateCategories(data);
        },
        error => console.log(error)
      )
  }

  deleteIncome(id) {
    this.incomesService.deleteIncome(id).subscribe(
      () => {
        this.incomesService.getIncomes().subscribe(
          data => {
            const formattedData = formatDateForTables(data);
            this.incomes = formattedData;
            this.chartIncomes = aggregateCategories(data);
            if (this.incomes.length <= Number(this.itemsPerPage)) {
              this.currentPage = "1";
            }
          },
          error => console.log(error)
        )   
      },
      error => console.log(error)
    );
  }

  addIncome() {
    this.router.navigate(['add-income']);
  }

}
