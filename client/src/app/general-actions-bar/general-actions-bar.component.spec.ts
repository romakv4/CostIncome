import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralActionsBarComponent } from './general-actions-bar.component';

describe('GeneralActionsBarComponent', () => {
  let component: GeneralActionsBarComponent;
  let fixture: ComponentFixture<GeneralActionsBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneralActionsBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneralActionsBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
