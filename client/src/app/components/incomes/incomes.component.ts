import { Component, OnInit } from '@angular/core';
import { IncomesService } from '../../services/incomes.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.css']
})
export class IncomesComponent implements OnInit {

  incomes = null;
  chartIncomes: Array<{}> = [];
  itemsPerPage = '14';
  currentPage = '1';
  inAdding = false
  inEditing = false
  incomeForEditId = null

  constructor(
    private incomesService: IncomesService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.incomesService.getIncomes()
      .subscribe(
        data => {
          this.incomes = data.formattedData;
          if (this.incomes.length === 0) {
            this.router.navigate(['/home'])
          }
          this.chartIncomes = data.chartCosts;
        },
        error => console.log(error)
      )
  }

  deleteIncome(id) {
    this.incomesService.deleteIncome(id).subscribe(
      () => {
        this.incomesService.getIncomes().subscribe(
          data => {
            this.incomes = data.formattedData;
            if (this.incomes.length === 0) {
              this.router.navigate(['/home'])
            }
            this.chartIncomes = data.chartCosts;
            if (this.incomes.length <= Number(this.itemsPerPage)) {
              this.currentPage = '1';
            }
          },
          error => console.log(error)
        )
      },
      error => console.log(error)
    );
  }

  addIncome() {
    this.inAdding = true
  }

  editIncome(id) {
    this.inEditing = true;
    this.incomeForEditId = id;
  }

  navigateToHome() {
    this.router.navigate(['/home']);
    return false;
  }

}
