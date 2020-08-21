import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingItemsTableComponent } from './accounting-items-table.component';
import { formatDateForTables } from '../../utils/formatDate';
import { By } from '@angular/platform-browser';

describe('AccountingItemsTableComponent', () => {
  let component: AccountingItemsTableComponent;
  let fixture: ComponentFixture<AccountingItemsTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountingItemsTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountingItemsTableComponent);
    component = fixture.componentInstance;
    component.accountingItems = formatDateForTables([
        {
            id: 1,
            category: 'Food',
            description: '',
            price: 100,
            date: new Date()
        }
    ]);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});