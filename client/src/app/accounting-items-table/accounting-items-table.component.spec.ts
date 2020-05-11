import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingItemsTableComponent } from './accounting-items-table.component';

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
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
