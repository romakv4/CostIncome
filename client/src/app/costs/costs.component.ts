import { Component, OnInit } from '@angular/core';
import { CostsService } from '../services/costs.service';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';
import { AccountingItem } from '../types/AccountingItem';
import { aggregateCategories } from '../utils/aggregateCategories';
import { formatDateForTables } from '../utils/formatDateForTables';

@Component({
  selector: 'app-costs',
  templateUrl: './costs.component.html',
  styleUrls: ['./costs.component.css']
})
export class CostsComponent implements OnInit {

  costs = null;
  chartCosts: Array<{}> = [];

  constructor(
    private costsService: CostsService,
    private tokenService: TokenService,
    private router: Router,
  ) { }

  ngOnInit() {
    if (!this.tokenService.isLoggedIn()) {
      this.router.navigate(['authorization']);
    }
    this.costsService.getCosts()
      .subscribe(
        (data: Array<AccountingItem>) => {
          const formattedData = formatDateForTables(data);
          this.costs = formattedData;
          this.chartCosts = aggregateCategories(data);
        },
        error => console.log(error)
      )
  }

  deleteCost(id) {
    this.costsService.deleteCost(id).subscribe(
      () => {
        this.costsService.getCosts().subscribe(
          data => {
            this.costs = data;
            this.chartCosts = aggregateCategories(data);
          },
          error => console.log(error)
        )   
      },
      error => console.log(error)
    );
  }

  addCost() {
    this.router.navigate(['add-cost']);
  }

}
