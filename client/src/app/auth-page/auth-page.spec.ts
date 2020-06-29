import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RouterTestingModule } from '@angular/router/testing';
import { AuthPageComponent } from './auth-page.component';
import { By } from '@angular/platform-browser';

describe('HomeComponent', () => {
  let component: AuthPageComponent;
  let fixture: ComponentFixture<AuthPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [ AuthPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have titles', () => {
      const h1 = fixture.debugElement.query(By.css('h1')).nativeElement;
      expect(h1.innerHTML.toUpperCase()).toBe('COST INCOME');
      const h2 = fixture.debugElement.query(By.css('h2')).nativeElement;
      expect(h2.innerHTML.toUpperCase()).toBe('SERVICE FOR FINANCIAL ACCOUNTING');
  });

  it('should have navigation', () => {
    const signUp = fixture.debugElement.query(By.css('a[href="/registration"]')).nativeElement;
    expect(signUp.innerHTML).toBe('Sign up');
    expect(signUp.hasAttribute('hidden')).toEqual(false);
    const signIn = fixture.debugElement.query(By.css('a[href="/authorization"]')).nativeElement;
    expect(signIn.innerHTML).toBe('Sign in');
    expect(signIn.hasAttribute('hidden')).toEqual(false);
  });
});