import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorsService } from '../services/errors.service';
import { AccountingItem, SetSuccess } from '../types/AccountingItem';
import { CostsService } from '../services/costs.service';

@Component({
  selector: 'app-add-cost-form',
  templateUrl: './add-cost-form.component.html',
  styleUrls: ['./add-cost-form.component.css']
})
export class AddCostFormComponent {

  addCostForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted: boolean = false;
  addCostSuccess: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private errorsService: ErrorsService,
    private costsService: CostsService,
  ) { 
    this.addCostForm = this.formBuilder.group({
      category: ['', [Validators.required, Validators.maxLength(20)]],
      description: ['', [Validators.maxLength(100)]],
      price: [Number(1), [Validators.required, Validators.min(1), Validators.max(79228162514264337593543950335)]],
      date: [Date(), [Validators.required]]
    })
  }

  get f() { return this.addCostForm.controls }

  onSubmit(costData: AccountingItem) {
    this.submitted = true;
    if (this.addCostForm.invalid) {
      return;
    }
    this.costsService.addCost(costData)
      .subscribe(
        (response: SetSuccess) => {
          this.addCostSuccess = response.success;
          if (this.addCostSuccess) {
            this.submitted = false;
            this.addCostForm.reset({
              category: "",
              description: null,
              price: 1,
              date: this.getCurrentDate()
            });
          }
          setTimeout(() => { this.addCostSuccess = null }, 2500);
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

  navigateToCostsList() {
    this.router.navigate(['/costs'])
  }

}
