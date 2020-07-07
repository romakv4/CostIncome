import { Component, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AccountingItem } from '../types/AccountingItem';
import { Parser } from 'json2csv';
import { saveAs } from 'file-saver';
import { formatDate } from '../utils/formatDate';

@Component({
  selector: 'app-general-actions-bar',
  templateUrl: './general-actions-bar.component.html',
  styleUrls: ['./general-actions-bar.component.css']
})
export class GeneralActionsBarComponent {

  @Input() isForTable: boolean;
  @Input() exportButtonTitle?: string;
  @Input() data?;
  @Input() reportType?: string;

  constructor(
    private authService: AuthService,
    private router: Router,
  ) { }

  onLogout() {
    this.authService.logout()
  }

  changePassword() {
    this.router.navigate(['changepassword'])
  }

  onExport(data: Array<AccountingItem>) {
    try {
      const fields = ['category', 'description', 'price', 'date'];
      const options = { fields };
      const parser = new Parser(options);
      const csv = parser.parse(data);
      const file = new Blob([csv], {type: 'text/csv;charset=windows-1251'});
      saveAs(file, `${this.reportType}_${formatDate(new Date())}.csv`);
    } catch (e) {
      console.error(e);
    }
  }

}
