import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ErrorsService } from '../../services/errors.service';
import { TokenService } from '../../services/token.service';
import { AccountingItem, OperationSuccess } from '../../types/AccountingItem';
import { formatDateForForms, formatDateForTables } from '../../utils/formatDate';
import { IncomesService } from '../../services/incomes.service';
import { aggregateCategories } from '../../utils/aggregateCategories';

@Component({
  selector: 'app-edit-income-form',
  templateUrl: './edit-income-form.component.html',
  styleUrls: ['./edit-income-form.component.css']
})
export class EditIncomeFormComponent implements OnInit {

  @Input() inEditing: boolean;
  @Output() inEditingChange = new EventEmitter<boolean>();

  @Input() incomeForEditId: number;

  @Input() incomes: any[]
  @Output() incomesChange = new EventEmitter<any[]>();

  @Input() chartIncomes: Array<{}>;
  @Output() chartIncomesChange = new EventEmitter<Array<{}>>();

  isEditedIncomeId;
  isEditedIncome;

  editIncomeForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted = false;
  editIncomeSuccess: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private errorsService: ErrorsService,
    private incomesService: IncomesService,
    private tokenService: TokenService,
  ) {
    this.editIncomeForm = this.formBuilder.group({
      category: ['', [Validators.required, Validators.maxLength(20)]],
      description: ['', [Validators.maxLength(20)]],
      price: [Number(1), [Validators.required, Validators.min(0.01), Validators.max(999999999999)]],
      date: [Date(), [Validators.required]]
    })
  }

  ngOnInit(): void {
    if (this.tokenService.isTokenExpired()) {
      this.router.navigate(['authorization']);
    }
    this.incomesService.getConcreteIncome(this.incomeForEditId)
      .subscribe(
        (data: AccountingItem) => {
          this.isEditedIncome = formatDateForForms(data);
          this.editIncomeForm.controls.category.setValue(data.category);
          this.editIncomeForm.controls.description.setValue(data.description);
          this.editIncomeForm.controls.price.setValue(data.price);
        }
      )
  }

  get f() { return this.editIncomeForm.controls }

  onSubmit(incomeData: AccountingItem) {
    this.submitted = true;
    if (this.editIncomeForm.invalid) {
      return;
    }
    incomeData.id = this.incomeForEditId;
    this.incomesService.editIncome(incomeData)
      .subscribe(
        (response: OperationSuccess) => {
          this.editIncomeSuccess = response.success;
          if (this.editIncomeSuccess) {
            this.refreshTable();
          }
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      )
  }

  refreshTable() {
    this.incomesService.getIncomes()
      .subscribe(
        data => {
          this.incomesChange.emit(data.formattedData);
          if (this.incomes.length === 0) {
            this.router.navigate(['/home'])
          }
          this.chartIncomesChange.emit(data.chartCosts);
          this.inEditingChange.emit(false);
        },
        error => console.log(error)
      )
  }

  toIncomes() {
    this.inEditingChange.emit(false);
  }

}
