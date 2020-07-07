import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddIncomeFormComponent } from './add-income-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { GeneralActionsBarComponent } from '../general-actions-bar/general-actions-bar.component';
import { Router } from '@angular/router';

describe('AddIncomeFormComponent', () => {
  let component: AddIncomeFormComponent;
  let fixture: ComponentFixture<AddIncomeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule
      ],
      declarations: [ AddIncomeFormComponent, GeneralActionsBarComponent ],
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
    fixture = TestBed.createComponent(AddIncomeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});