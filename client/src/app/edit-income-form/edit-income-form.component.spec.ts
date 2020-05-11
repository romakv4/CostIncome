import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditIncomeFormComponent } from './edit-income-form.component';

describe('EditIncomeFormComponent', () => {
  let component: EditIncomeFormComponent;
  let fixture: ComponentFixture<EditIncomeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditIncomeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditIncomeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
