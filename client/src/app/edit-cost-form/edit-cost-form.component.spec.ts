import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCostFormComponent } from './edit-cost-form.component';

describe('EditCostFormComponent', () => {
  let component: EditCostFormComponent;
  let fixture: ComponentFixture<EditCostFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCostFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCostFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
