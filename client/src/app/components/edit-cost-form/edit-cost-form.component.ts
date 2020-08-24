import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorsService } from '../../services/errors.service';
import { CostsService } from '../../services/costs.service';
import { AccountingItem, OperationSuccess } from '../../types/AccountingItem';
import { formatDateForForms } from '../../utils/formatDate';

@Component({
  selector: 'app-edit-cost-form',
  templateUrl: './edit-cost-form.component.html',
  styleUrls: ['./edit-cost-form.component.css']
})
export class EditCostFormComponent implements OnInit {

  @Input() inEditing: boolean;
  @Output() inEditingChange = new EventEmitter<boolean>();

  @Input() costForEditId: number;

  @Input() costs: any[]
  @Output() costsChange = new EventEmitter<any[]>();

  @Input() chartCosts: Array<{}>;
  @Output() chartCostsChange = new EventEmitter<Array<{}>>();

  isEditedCostId;
  isEditedCost;

  editCostForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted = false;
  editCostSuccess: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private errorsService: ErrorsService,
    private costsService: CostsService,
  ) { }

  ngOnInit(): void {
    this.editCostForm = this.formBuilder.group({
      category: ['', [Validators.required, Validators.maxLength(20)]],
      description: ['', [Validators.maxLength(20)]],
      price: [Number(1), [Validators.required, Validators.min(0.01), Validators.max(999999999999)]],
      date: [Date(), [Validators.required]]
    })
    this.costsService.getConcreteCost(this.costForEditId)
      .subscribe(
        (data: AccountingItem) => {
          this.isEditedCost = formatDateForForms(data);
          this.f.category.setValue(this.isEditedCost.category);
          this.f.description.setValue(this.isEditedCost.description);
          this.f.price.setValue(this.isEditedCost.price);
          this.f.date.setValue(this.isEditedCost.date);
        }
      )
  }

  get f() { return this.editCostForm.controls }

  onSubmit(costData: AccountingItem) {
    this.submitted = true;
    if (this.editCostForm.invalid) {
      return;
    }
    console.log(costData);
    costData.id = this.costForEditId;
    this.costsService.editCost(costData)
      .subscribe(
        (response: OperationSuccess) => {
          this.editCostSuccess = response.success;
          if (this.editCostSuccess) {
            this.refreshTable();
          }
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
          this.inEditingChange.emit(false);
        },
        error => console.log(error)
      )
  }

  toCosts() {
    this.inEditingChange.emit(false);
  }

}
