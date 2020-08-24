import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CostsService } from '../../services/costs.service';
import { IncomesService } from '../../services/incomes.service';

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
    private router: Router,
    private costsService: CostsService,
    private incomesService: IncomesService,
  ) { }

  ngOnInit(): void {
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

  navigateToCosts() {
    this.router.navigate(['/costs']);
    return false;
  }

  navigateToAddCost() {
    this.router.navigate(['/add-cost']);
    return false;
  }

  navigateToIncomes() {
    this.router.navigate(['/incomes']);
    return false;
  }

  navigateToAddIncome() {
    this.router.navigate(['/add-income']);
    return false;
  }
}
