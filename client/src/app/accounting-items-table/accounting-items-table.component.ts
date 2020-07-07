import { Component, Input } from '@angular/core';
import { AccountingItem } from '../types/AccountingItem';

@Component({
  selector: 'app-accounting-items-table',
  templateUrl: './accounting-items-table.component.html',
  styleUrls: ['./accounting-items-table.component.css']
})
export class AccountingItemsTableComponent {

  @Input() caption: string;
  @Input() accountingItems: Array<AccountingItem>
  @Input() withActions: boolean
  @Input() deleteAction: () => void
  @Input() accountingItemsService: any

}
