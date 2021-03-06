import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorsService } from '../../services/errors.service';
import { IncomesService } from '../../services/incomes.service';
import { AccountingItem, OperationSuccess } from '../../types/AccountingItem';
import { getCurrentDate } from 'src/app/utils/formatDate';

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
  ) { }

  ngOnInit(): void {
    this.addIncomeForm = this.formBuilder.group({
      category: ['', [Validators.required, Validators.maxLength(20)]],
      description: ['', [Validators.maxLength(20)]],
      price: [Number(1), [Validators.required, Validators.min(0.01), Validators.max(999999999999)]],
      date: [Date(), [Validators.required]]
    })
    this.f.date.setValue(getCurrentDate());
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
              date: getCurrentDate()
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
        data => {
          this.incomesChange.emit(data.formattedData);
          if (this.incomes.length === 0) {
            this.router.navigate(['/home'])
          }
          this.chartIncomesChange.emit(data.chartCosts);
        },
        error => console.log(error)
      )
  }

  toIncomes() {
    if (this.router.url === '/add-income') {
      this.router.navigate(['/incomes'])
    } else {
      this.inAddingChange.emit(false);
    }
  }

}
