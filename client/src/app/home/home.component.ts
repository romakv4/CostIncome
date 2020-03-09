import { Component, OnInit } from '@angular/core';
import { CostsService } from '../services/costs.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(
    private costsService: CostsService,
  ) { }

  ngOnInit() {
    this.costsService.getCosts()
      .subscribe(
        data => console.log(data)
      )
  }

}
