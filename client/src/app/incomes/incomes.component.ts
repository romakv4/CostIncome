import { Component, OnInit } from '@angular/core';
import { IncomesService } from '../services/incomes.service';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.css']
})
export class IncomesComponent implements OnInit {

  incomes = null;

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
        data => {
          this.incomes = data;
        },
        error => console.log(error)
      )
  }

  deleteIncome(id) {
    this.incomesService.deleteIncome(id).subscribe(
      () => {
        this.incomesService.getIncomes().subscribe(
          data => {
            this.incomes = data;
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
