import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCostFormComponent } from './add-cost-form.component';

describe('AddCostFormComponent', () => {
  let component: AddCostFormComponent;
  let fixture: ComponentFixture<AddCostFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddCostFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCostFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
