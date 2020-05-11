import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent {

  @Input() chartData;

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
    console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

}
