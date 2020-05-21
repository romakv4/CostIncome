import { Component, Input, OnInit } from '@angular/core';
import { CostsService } from '../services/costs.service';
import { Router } from '@angular/router';
import { IncomesService } from '../services/incomes.service';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent {

  @Input() chartData;

  constructor(
    private router: Router,
    private costsService: CostsService,
    private incomesService: IncomesService
  ) { }

  view: any[] = [700, 500];
  gradient: boolean = true;
  showLegend: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  legendPosition: string = 'below';
  trimLabels: boolean = false;

  ngAfterViewInit(): void {
    const elements = document.getElementsByClassName('legend-labels horizontal-legend');
    if (elements.length > 1) {
      console.log('Your method of legend css overriding was broken :(');
    }
    for (let i = 0; i < elements.length; i++) {
      const legend = elements[i] as HTMLElement;
      legend.style.whiteSpace = 'inherit';
    }
  }

  onSelect(data): void {
    switch(this.router.url) {
      case '/costs':
        this.costsService.getCostsByCategory(data.name)
          .subscribe(
            data => console.log(data),
            error => console.log(error)
          )
      case '/incomes':
        this.incomesService.getIncomesByCategory(data.name)
          .subscribe(
            data => console.log(data),
            error => console.log(error)
          )
    }
  }

  onActivate(data): void {
    // console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data): void {
    // console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

}
