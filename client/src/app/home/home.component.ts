import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { CostsService } from '../services/costs.service';
import { IncomesService } from '../services/incomes.service';
import { AccountingItem } from '../types/AccountingItem';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  costs = null;
  incomes = null;

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
          this.costs = data.slice(Math.max(data.length - 10, 0));
        },
        error => console.log(error)
      )
    this.incomesService.getIncomes()
      .subscribe(
        (data: Array<AccountingItem>) => {
          this.incomes = data.slice(Math.max(data.length - 10, 0));
        },
        error => console.log(error)
      )
  }

  private toCosts() {
    this.router.navigate(["/costs"])
  }

  private toIncomes() {
    this.router.navigate(["/incomes"])
  }

}
