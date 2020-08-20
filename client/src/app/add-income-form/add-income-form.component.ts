import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorsService } from '../services/errors.service';
import { IncomesService } from '../services/incomes.service';
import { AccountingItem, OperationSuccess } from '../types/AccountingItem';
import { TokenService } from '../services/token.service';
import { formatDateForTables } from '../utils/formatDate';
import { aggregateCategories } from '../utils/aggregateCategories';

@Component({
  selector: 'app-add-income-form',
  templateUrl: './add-income-form.component.html',
  styleUrls: ['./add-income-form.component.css']
})
export class AddIncomeFormComponent implements OnInit {

  @Input() inAdding: boolean;
  @Output() inAddingChange = new EventEmitter<boolean>();

  @Input() incomes: any[]
  @Output() incomesChange = new EventEmitter<any[]>();

  @Input() chartIncomes: Array<{}>;
  @Output() chartIncomesChange = new EventEmitter<Array<{}>>();

  addIncomeForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted = false;
  addIncomeSuccess: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private errorsService: ErrorsService,
    private incomesService: IncomesService,
    private tokenService: TokenService,
  ) {
    this.addIncomeForm = this.formBuilder.group({
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
  }

  get f() { return this.addIncomeForm.controls }

  onSubmit(incomeData: AccountingItem) {
    this.submitted = true;
    if (this.addIncomeForm.invalid) {
      return;
    }
    this.incomesService.addIncome(incomeData)
      .subscribe(
        (response: OperationSuccess) => {
          this.addIncomeSuccess = response.success;
          if (this.addIncomeSuccess) {
            this.submitted = false;
            this.addIncomeForm.reset({
              category: '',
              description: null,
              price: 1,
              date: this.getCurrentDate()
            });
          }
          setTimeout(() => { this.addIncomeSuccess = null }, 2500);
          this.refreshTable()
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      )
  }

  refreshTable() {
    this.incomesService.getIncomes()
      .subscribe(
        (data: Array<AccountingItem>) => {
          const formattedData = formatDateForTables(data);
          this.incomesChange.emit(formattedData);
          if (this.incomes.length === 0) {
            this.router.navigate(['/home'])
          }
          this.chartIncomesChange.emit(aggregateCategories(data));
        },
        error => console.log(error)
      )
  }

  getCurrentDate() {
    const date = new Date();
    return date.getFullYear().toString() + '-'
        + (date.getMonth() + 1).toString().padStart(2, '0') + '-'
        + date.getDate().toString().padStart(2, '0');
  }

  toIncomes() {
    if (this.router.url === '/add-income') {
      this.router.navigate(['/incomes'])
    } else {
      this.inAddingChange.emit(false);
    }
  }

}
