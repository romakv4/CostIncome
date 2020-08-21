import { Component, Input } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { AccountingItem } from '../../types/AccountingItem';
import { formatDate } from '../../utils/formatDate';

import * as xlsx from 'json-as-xlsx';

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
      const columns = [
        { label: 'Category', value: row => (row.category) },
        { label: 'Description', value: row => (row.description) },
        { label: 'Price', value: row => (row.price) },
        { label: 'Date', value: row => (row.date) }
      ]

      const settings = {
        sheetName: `${this.reportType}_${formatDate(new Date())}`,
        fileName: `${this.reportType}_${formatDate(new Date())}`
      }

      xlsx(columns, data, settings);
    } catch (e) {
      console.error(e);
    }
  }

}
