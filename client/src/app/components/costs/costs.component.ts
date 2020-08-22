import { Component, OnInit } from '@angular/core';
import { CostsService } from '../../services/costs.service';
import { Router } from '@angular/router';

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
  inEditing = false;
  costForEditId = null;

  constructor(
    private costsService: CostsService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.costsService.getCosts()
      .subscribe(
        data => {
          this.costs = data.formattedData;
          if (this.costs.length === 0) {
            this.router.navigate(['/home'])
          }
          this.chartCosts = data.chartCosts;
        },
        error => console.log(error)
      )
  }

  deleteCost(id) {
    this.costsService.deleteCost(id).subscribe(
      () => {
        this.costsService.getCosts().subscribe(
          data => {
            this.costs = data.formattedData;
            if (this.costs.length === 0) {
              this.router.navigate(['/home'])
            }
            this.chartCosts = data.chartCosts;
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
    this.inEditing = true;
    this.costForEditId = id;
  }

}
