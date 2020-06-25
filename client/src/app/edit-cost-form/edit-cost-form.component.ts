import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ErrorsService } from '../services/errors.service';
import { CostsService } from '../services/costs.service';
import { TokenService } from '../services/token.service';
import { AccountingItem, OperationSuccess } from '../types/AccountingItem';
import { formatDateForForms } from '../utils/formatDate';

@Component({
  selector: 'app-edit-cost-form',
  templateUrl: './edit-cost-form.component.html',
  styleUrls: ['./edit-cost-form.component.css']
})
export class EditCostFormComponent implements OnInit {

  isEditedCostId;
  isEditedCost;

  editCostForm;
  serverErrors;
  resetServerErrors = this.errorsService.resetServerErrors;
  submitted: boolean = false;
  editCostSuccess: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private errorsService: ErrorsService,
    private costsService: CostsService,
    private tokenService: TokenService,
    private route: ActivatedRoute,
  ) { 
    this.editCostForm = this.formBuilder.group({
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
    this.route.paramMap.subscribe(params => {
      this.isEditedCostId = Number.parseInt(params.get("id"));
    });
    this.costsService.getConcreteCost(this.isEditedCostId)
      .subscribe(
        (data: AccountingItem) => {
          this.isEditedCost = formatDateForForms(data);
          this.editCostForm.controls['category'].setValue(data.category);
          this.editCostForm.controls['description'].setValue(data.description);
          this.editCostForm.controls['price'].setValue(data.price);
        }
      )
  }

  get f() { return this.editCostForm.controls }

  onSubmit(costData: AccountingItem) {
    this.submitted = true;
    if (this.editCostForm.invalid) {
      return;
    }
    costData.id = this.isEditedCostId;
    this.costsService.editCost(costData)
      .subscribe(
        (response: OperationSuccess) => {
          this.editCostSuccess = response.success;
          if (this.editCostSuccess) {
            this.router.navigate(['/costs'])
          }
        },
        errorResponse => { this.serverErrors = errorResponse.error }
      )
  }

}
