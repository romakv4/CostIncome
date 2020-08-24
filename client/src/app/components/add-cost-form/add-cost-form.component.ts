import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorsService } from '../../services/errors.service';
import { AccountingItem, OperationSuccess } from '../../types/AccountingItem';
import { CostsService } from '../../services/costs.service';
import { getCurrentDate } from 'src/app/utils/formatDate';
import { trigger, transition, animate, state, style } from '@angular/animations';

@Component({
  selector: 'app-add-cost-form',
  templateUrl: './add-cost-form.component.html',
  styleUrls: ['./add-cost-form.component.css'],
})
export class AddCostFormComponent implements OnInit {

  @Input() inAdding: boolean;
  @Output() inAddingChange = new EventEmitter<boolean>();

  @Input() costs: any[]
  @Output() costsChange = new EventEmitter<any[]>();

  @Input() chartCosts: Array<{}>;
  @Output() chartCostsChange = new EventEmitter<Array<{}>>();

  addCostForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted = false;
  addCostSuccess: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private errorsService: ErrorsService,
    private costsService: CostsService,
  ) { }

  ngOnInit(): void {
    this.addCostForm = this.formBuilder.group({
      category: ['', [Validators.required, Validators.maxLength(20)]],
      description: ['', [Validators.maxLength(20)]],
      price: [Number(1), [Validators.required, Validators.min(0.01), Validators.max(999999999999)]],
      date: [Date(), [Validators.required]]
    })
    this.f.date.setValue(getCurrentDate());
  }

  get f() { return this.addCostForm.controls }

  onSubmit(costData: AccountingItem) {
    this.submitted = true;
    if (this.addCostForm.invalid) {
      return;
    }
    this.costsService.addCost(costData)
      .subscribe(
        (response: OperationSuccess) => {
          this.addCostSuccess = response.success;
          if (this.addCostSuccess) {
            this.submitted = false;
            this.addCostForm.reset({
              category: '',
              description: null,
              price: 1,
              date: getCurrentDate()
            });
          }
          setTimeout(() => { this.addCostSuccess = null }, 2500);
          this.refreshTable();
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      )
  }

  refreshTable() {
    this.costsService.getCosts()
      .subscribe(
        data => {
          this.costsChange.emit(data.formattedData);
          if (this.costs.length === 0) {
            this.router.navigate(['/home'])
          }
          this.chartCostsChange.emit(data.chartCosts);
        },
        error => console.log(error)
      )
  }

  toCosts() {
    if (this.router.url === '/add-cost') {
      this.router.navigate(['/costs'])
    } else {
      this.inAddingChange.emit(false);
    }
  }

}
