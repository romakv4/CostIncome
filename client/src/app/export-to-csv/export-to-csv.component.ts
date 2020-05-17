import { Component, Input } from '@angular/core';
import { Parser } from "json2csv";
import { saveAs } from "file-saver";
import { AccountingItem } from '../types/AccountingItem';
import { formatDate } from '../utils/formatDate';

@Component({
  selector: 'app-export-to-csv',
  templateUrl: './export-to-csv.component.html',
  styleUrls: ['./export-to-csv.component.css']
})
export class ExportToCsvComponent {

  @Input() data;
  @Input() reportType: String;

  constructor() { }

  onExport(data: Array<AccountingItem>) {
    try {
      const fields = ['category', 'description', 'price', 'date'];
      const options = { fields };
      const parser = new Parser(options);
      const csv = parser.parse(data);
      const file = new Blob([csv], {type: "text/csv;charset=windows-1251"});
      saveAs(file, `${this.reportType}_${formatDate(new Date())}.csv`);
    } catch (e) {
      console.error(e);
    }
  }

  

}
