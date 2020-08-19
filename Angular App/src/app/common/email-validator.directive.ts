import { Directive } from '@angular/core';
import { NG_VALIDATORS, EmailValidator, Validator, ValidatorFn, FormControl } from '@angular/forms';

@Directive({
  selector: '[emailValidator][ngModel]',
  providers: [{provide: NG_VALIDATORS, useExisting:EmailValidator, multi: true}]
})

export class EmailValidatorDirective implements Validator{
  validator: ValidatorFn;
 
  constructor() { 
    this.validator = this.emailValidator()
  }

  validate(c: FormControl) {  
    return this.validator(c);  
   }  

  emailValidator(): ValidatorFn {  
    return (c: FormControl) => {  
     let isValid = /^[_a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/.test(c.value);  
     if (isValid) {  
      return null;  
     } else {  
      return {  
       emailvalidator: {  
        valid: false  
       }  
      };  
     }  
    }  
   } 
}
