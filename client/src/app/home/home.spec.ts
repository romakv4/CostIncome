import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { GeneralActionsBarComponent } from '../general-actions-bar/general-actions-bar.component';
import { AccountingItemsTableComponent } from '../accounting-items-table/accounting-items-table.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      declarations: [ HomeComponent, GeneralActionsBarComponent, AccountingItemsTableComponent ],
      providers: [
        {
          provide: Router,
          useValue: { navigate: jasmine.createSpy('navigate') }
        }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    let actionsBar = TestBed.createComponent(GeneralActionsBarComponent);
    actionsBar.componentInstance.isForTable = false;
    let costsTable = TestBed.createComponent(AccountingItemsTableComponent);
    costsTable.componentInstance.caption = "Costs";
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});