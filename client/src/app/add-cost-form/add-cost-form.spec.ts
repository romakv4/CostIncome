import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCostFormComponent } from './add-cost-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { GeneralActionsBarComponent } from '../general-actions-bar/general-actions-bar.component';
import { Router } from '@angular/router';
import { AccountingItemsTableComponent } from '../accounting-items-table/accounting-items-table.component';

describe('AddCostFormComponent', () => {
  let component: AddCostFormComponent;
  let fixture: ComponentFixture<AddCostFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule
      ],
      declarations: [ AddCostFormComponent, GeneralActionsBarComponent ],
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
    const actionsBar = TestBed.createComponent(GeneralActionsBarComponent);
    actionsBar.componentInstance.isForTable = false;
    fixture = TestBed.createComponent(AddCostFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});