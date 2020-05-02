import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddIncomeFormComponent } from './add-income-form.component';

describe('AddIncomeFormComponent', () => {
  let component: AddIncomeFormComponent;
  let fixture: ComponentFixture<AddIncomeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddIncomeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddIncomeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
