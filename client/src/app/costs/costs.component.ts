import { Component, OnInit } from '@angular/core';
import { CostsService } from '../services/costs.service';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';
// import { IncomesService } from '../services/incomes.service';

@Component({
  selector: 'app-costs',
  templateUrl: './costs.component.html',
  styleUrls: ['./costs.component.css']
})
export class CostsComponent implements OnInit {

  costs = null;
  
  // incomes = null;
  // firstTenIncomes = [];

  constructor(
    private costsService: CostsService,
    // private incomesService: IncomesService,
    private tokenService: TokenService,
    private router: Router,
  ) { }

  ngOnInit() {
    if (!this.tokenService.isLoggedIn()) {
      this.router.navigate(['authorization']);
    }
    this.costsService.getCosts()
      .subscribe(
        data => {
          this.costs = data;
        },
        error => console.log(error)
      )
    // this.incomesService.getIncomes()
    //   .subscribe(
    //     data => {
    //       this.incomes = data;
    //       this.firstTenIncomes = this.incomes.slice(-10);
    //     },
    //     error => console.log(error)
    //   )
  }

  deleteCost(id) {
    this.costsService.deleteCost(id).subscribe(
      () => {
        this.costsService.getCosts().subscribe(
          data => {
            this.costs = data;
          },
          error => console.log(error)
        )   
      },
      error => console.log(error)
    );
  }

  // deleteIncome(id) {
  //   this.incomesService.deleteIncome(id).subscribe(
  //     () => {
  //       this.incomesService.getIncomes().subscribe(
  //         data => {
  //           this.incomes = data;
  //           this.firstTenIncomes = this.incomes.slice(-10);
  //         },
  //         error => console.log(error)
  //       )   
  //     },
  //     error => console.log(error)
  //   );
  // }

  addRecord(type) {
    if (type === "cost") {
      this.router.navigate([`add-${type}`]);
    }
  }

}
