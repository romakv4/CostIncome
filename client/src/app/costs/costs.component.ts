import { Component, OnInit } from '@angular/core';
import { CostsService } from '../services/costs.service';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';
import { AccountingItem } from '../types/AccountingItem';
import { aggregateCategories } from '../utils/aggregateCategories';
import { formatDateForTables } from '../utils/formatDate';

@Component({
  selector: 'app-costs',
  templateUrl: './costs.component.html',
  styleUrls: ['./costs.component.css']
})
export class CostsComponent implements OnInit {

  costs = null;
  chartCosts: Array<{}> = [];
  itemsPerPage = '14';
  currentPage = '1';
  inAdding = false;

  constructor(
    private costsService: CostsService,
    private tokenService: TokenService,
    private router: Router,
  ) { }

  ngOnInit() {
    if (this.tokenService.isTokenExpired()) {
      this.router.navigate(['authorization']);
    }
    this.costsService.getCosts()
      .subscribe(
        (data: Array<AccountingItem>) => {
          const formattedData = formatDateForTables(data);
          this.costs = formattedData;
          if (this.costs.length === 0) {
            this.router.navigate(['/home'])
          }
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
            const formattedData = formatDateForTables(data);
            this.costs = formattedData;
            if (this.costs.length === 0) {
              this.router.navigate(['/home'])
            }
            this.chartCosts = aggregateCategories(data);
            if (this.costs.length <= Number(this.itemsPerPage)) {
              this.currentPage = '1';
            }
          },
          error => console.log(error)
        )
      },
      error => console.log(error)
    );
  }

  addCost() {
    this.inAdding = true
  }

  editCost(id) {
    this.router.navigate(['/edit-cost', id]);
  }

}
