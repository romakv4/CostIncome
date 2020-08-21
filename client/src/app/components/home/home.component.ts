import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from '../../services/token.service';
import { CostsService } from '../../services/costs.service';
import { IncomesService } from '../../services/incomes.service';
import { AccountingItem } from '../../types/AccountingItem';
import { formatDateForTables } from '../../utils/formatDate';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  costs = [];
  incomes = [];
  costsClicked = false
  incomesClicked = false

  constructor(
    private tokenService: TokenService,
    private router: Router,
    private costsService: CostsService,
    private incomesService: IncomesService,
  ) { }

  ngOnInit(): void {
    if (this.tokenService.isTokenExpired()) {
      this.router.navigate(['authorization']);
    }
    this.costsService.getCosts()
      .subscribe(
        data => {
          this.costs = data.formattedData.slice(0, 10);
        },
        error => console.log(error)
      )
    this.incomesService.getIncomes()
      .subscribe(
        data => {
          this.incomes = data.formattedData.slice(0, 10);
        },
        error => console.log(error)
      )
  }
}
