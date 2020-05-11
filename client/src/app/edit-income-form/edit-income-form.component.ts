import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ErrorsService } from '../services/errors.service';
import { CostsService } from '../services/costs.service';
import { TokenService } from '../services/token.service';
import { AccountingItem, OperationSuccess } from '../types/AccountingItem';
import { formatDateForForms } from '../utils/formatDate';
import { IncomesService } from '../services/incomes.service';

@Component({
  selector: 'app-edit-income-form',
  templateUrl: './edit-income-form.component.html',
  styleUrls: ['./edit-income-form.component.css']
})
export class EditIncomeFormComponent implements OnInit {

  isEditedIncomeId;
  isEditedIncome;

  editIncomeForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted: boolean = false;
  editIncomeSuccess: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private errorsService: ErrorsService,
    private incomesService: IncomesService,
    private tokenService: TokenService,
    private route: ActivatedRoute,
  ) { 
    this.editIncomeForm = this.formBuilder.group({
      category: ['', [Validators.required, Validators.maxLength(20)]],
      description: ['', [Validators.maxLength(20)]],
      price: [Number(1), [Validators.required, Validators.min(0.01), Validators.max(999999999999)]],
      date: [Date(), [Validators.required]]
    })
  }

  ngOnInit(): void {
    if (!this.tokenService.isLoggedIn()) {
      this.router.navigate(['authorization']);
    }
    this.route.paramMap.subscribe(params => {
      this.isEditedIncomeId = params.get("id");
    });
    this.incomesService.getConcreteIncome(this.isEditedIncomeId)
      .subscribe(
        (data: AccountingItem) => {
          this.isEditedIncome = formatDateForForms(data);
          this.editIncomeForm.controls['category'].setValue(data.category);
          this.editIncomeForm.controls['description'].setValue(data.description);
          this.editIncomeForm.controls['price'].setValue(data.price);
        }
      )
  }

  get f() { return this.editIncomeForm.controls }

  onSubmit(incomeData: AccountingItem) {
    this.submitted = true;
    if (this.editIncomeForm.invalid) {
      return;
    }
    incomeData.id = this.isEditedIncomeId;
    this.incomesService.editIncome(incomeData)
      .subscribe(
        (response: OperationSuccess) => {
          this.editIncomeSuccess = response.success;
          if (this.editIncomeSuccess) {
            this.router.navigate(['/incomes'])
          }
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      )
  }

}
