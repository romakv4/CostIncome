import { Component, Input, OnInit, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements AfterViewInit {

  @Input() chartData;

  view: any[] = [700, 500];
  gradient = true;
  showLegend = true;
  showLabels = true;
  isDoughnut = false;
  legendPosition = 'below';
  trimLabels = false;

  ngAfterViewInit(): void {
    const elements = Array.from(document.getElementsByClassName('legend-labels horizontal-legend'));
    if (elements.length > 1) {
      console.log('Your method of legend css overriding was broken :(');
    }

    for (const element of elements) {
      (element as HTMLElement).style.whiteSpace = 'inherit';
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
