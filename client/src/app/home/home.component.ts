import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { CostsService } from '../services/costs.service';
import { IncomesService } from '../services/incomes.service';
import { AccountingItem } from '../types/AccountingItem';
import { formatDateForTables } from '../utils/formatDateForTables';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  costs = [];
  incomes = [];

  constructor(
    private tokenService: TokenService,
    private router: Router,
    private costsService: CostsService,
    private incomesService: IncomesService
  ) { }

  ngOnInit(): void {
    if (!this.tokenService.isLoggedIn()) {
      this.router.navigate(['authorization']);
    }
    this.costsService.getCosts()
      .subscribe(
        (data: Array<AccountingItem>) => {
          const formattedData = formatDateForTables(data.slice(Math.max(data.length - 10, 0)));
          this.costs = formattedData;
        },
        error => console.log(error)
      )
    this.incomesService.getIncomes()
      .subscribe(
        (data: Array<AccountingItem>) => {
          const formattedData = formatDateForTables(data.slice(Math.max(data.length - 10, 0)));
          this.incomes = formattedData;
        },
        error => console.log(error)
      )
  }
}
