import { Validator, NG_VALIDATORS, AbstractControl, ValidationErrors } from '@angular/forms';
import { Directive, Input } from '@angular/core';

@Directive({
  selector: '[appValidateEqualValue]',
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: ValidateEqualValueDirective,
    multi: true
  }]
})
export class ValidateEqualValueDirective implements Validator {

  @Input() appValidateEqualValue;

  constructor() { }

  validate(control: AbstractControl): ValidationErrors {
    const controlToCompare = control.parent.get(this.appValidateEqualValue);
    if (controlToCompare && controlToCompare.value !== control.value) {
      return { notEqual: true };
    }
  }

}
