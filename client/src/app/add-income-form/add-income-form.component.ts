import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorsService } from '../services/errors.service';
import { IncomesService } from '../services/incomes.service';
import { AccountingItem, SetSuccess } from '../types/AccountingItem';

@Component({
  selector: 'app-add-income-form',
  templateUrl: './add-income-form.component.html',
  styleUrls: ['./add-income-form.component.css']
})
export class AddIncomeFormComponent {

  addIncomeForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted: boolean = false;
  addIncomeSuccess: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private errorsService: ErrorsService,
    private costsService: IncomesService,
  ) { 
    this.addIncomeForm = this.formBuilder.group({
      category: ['', [Validators.required, Validators.maxLength(20)]],
      description: ['', [Validators.maxLength(100)]],
      price: [Number(1), [Validators.required, Validators.min(1), Validators.max(79228162514264337593543950335)]],
      date: [Date(), [Validators.required]]
    })
  }

  get f() { return this.addIncomeForm.controls }

  onSubmit(incomeData: AccountingItem) {
    this.submitted = true;
    if (this.addIncomeForm.invalid) {
      return;
    }
    this.costsService.addIncome(incomeData)
      .subscribe(
        (response: SetSuccess) => {
          this.addIncomeSuccess = response.success;
          if (this.addIncomeSuccess) {
            this.submitted = false;
            this.addIncomeForm.reset({
              category: "",
              description: null,
              price: 1,
              date: this.getCurrentDate()
            });
          }
          setTimeout(() => { this.addIncomeSuccess = null }, 2500);
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      )
  }

  getCurrentDate() {
    const date = new Date();
    return date.getFullYear().toString() + '-'
        + (date.getMonth() + 1).toString().padStart(2, '0') + '-'
        + date.getDate().toString().padStart(2, '0');
  }

  navigateToIncomesList() {
    this.router.navigate(['/incomes'])
  }

}
