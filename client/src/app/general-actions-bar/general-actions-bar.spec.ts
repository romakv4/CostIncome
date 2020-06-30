import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralActionsBarComponent } from './general-actions-bar.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { By } from '@angular/platform-browser';
import { MockRender } from 'ng-mocks';

describe('GeneralActionsBarComponent', () => {
  let component: GeneralActionsBarComponent;
  let fixture: ComponentFixture<GeneralActionsBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ RouterTestingModule, HttpClientTestingModule ],
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

  it('should have 2 buttons', () => {
    expect(fixture.debugElement.queryAll(By.css('button')).length).toEqual(2);
    const logout = fixture.debugElement.query(By.css('button[class="logOut"]')).nativeElement;
    expect(logout.innerHTML).toBe('Log out');
    const changePassword = fixture.debugElement.query(By.css('button[class="changePassword"]')).nativeElement;
    expect(changePassword.innerHTML).toBe('Change password');
  });

  it('should have 3 buttons', () => {
    const fixture = MockRender(
      `<app-general-actions-bar
          [isForTable]="true"
          [exportButtonTitle]="'Export all costs data as CSV file'"
      ></app-general-actions-bar>`
    );
    expect(fixture.debugElement.queryAll(By.css('button')).length).toEqual(3);
    const logout = fixture.debugElement.query(By.css('button[class="logOut"]')).nativeElement;
    expect(logout.innerHTML).toBe('Log out');
    const changePassword = fixture.debugElement.query(By.css('button[class="changePassword"]')).nativeElement;
    expect(changePassword.innerHTML).toBe('Change password');
    const exportToCsv = fixture.debugElement.query(By.css('button[class="exportToCsv"]')).nativeElement;
    expect(exportToCsv.innerHTML).toBe('Export to CSV');
  });

  it('Logout function calling', () => {
    const logout = fixture.debugElement.query(By.css('button[class="logOut"]')).nativeElement;
    spyOn(component, 'onLogout').and.callThrough();
    logout.click();
    expect(component.onLogout).toHaveBeenCalledTimes(1);
  });

  it('change password function calling', () => {
    const changePassword = fixture.debugElement.query(By.css('button[class="changePassword"]')).nativeElement;
    spyOn(component, 'changePassword').and.callThrough();
    changePassword.click();
    expect(component.changePassword).toHaveBeenCalledTimes(1);
  });
});