import { AbstractControl, ValidatorFn as formsValidator} from '@angular/forms';

export class ValidatorFn{

  static number(): formsValidator {
    return (control:AbstractControl): {[key:string]:boolean} | null =>{
  
      if(control.value != null) {    
        return this.isNumber(control.value);
      }else{
        return null;
      }      
    }
  }

  static textlength(minlength: number, maxLength: number): formsValidator {
    return (control:AbstractControl): {[key:string]:boolean} | null =>{
  
      if(control.value != null) {
        //if(control.value.match(/^([9]{1})([234789]{1})([0-9]{8})$/))
        if( control.value.length != 0 && (control.value.length < minlength || control.value.length > maxLength)) {
          return {'textlength': true};
        }
      }  
      return null;
    }
  }

  static range(minNumber: number, maxNumber: number): formsValidator {
    return (control:AbstractControl): {[key:string]:boolean} | null =>{
  
      if(control.value != null) {
        if(isNaN(control.value)) {
          return {'number': true};
        }
        if(control.value < minNumber || control.value > maxNumber) {
          return {'range': true};
        }
      }  
      return null;
    }
  }

  static text(control:AbstractControl): {[key:string]:boolean} | null {
        if(control.value === null || control.value.length == 0  || (control.value.match(/^[a-z,A-Z]+$/)) || (control.pristine )){
            return null;
        }
        return {'text': true};
    }
  
  static emailMatcherValidation(firstControlName: string, secondControlName: string): formsValidator {
    return (control:AbstractControl): {[key:string]:boolean} | null =>{
  
        let firstEmail = control.get(firstControlName);
        let secondEmail = control.get(secondControlName);
    
        if((firstEmail.value === secondEmail.value) || (firstEmail.pristine || secondEmail.pristine)){
            return null;
        }
        return {'match': true};
    }
  }

  private static isNumber(value: number): {[key:string]:boolean} | null
  {
    if(isNaN(value)) {
      return {'number': true};
    }else{
      return null;
    }
  }
}
// function mobileValidation(minlength: number, maxLength: number): ValidatorFn {
//     return (control:AbstractControl): {[key:string]:boolean} | null =>{

//       if(control.value != null) {    
//         if(isNaN(control.value)) {
//           return {'notnumber': true};
//         }
//         //if(control.value.match(/^([9]{1})([234789]{1})([0-9]{8})$/))
//         if( control.value.length != 0 && (control.value.length < minlength || control.value.length > maxLength)) {
//           return {'invalidlength': true};
//         }
//       }  
//       return null;
//     }
// }

// function textValidation(control:AbstractControl): {[key:string]:boolean} | null {
//         if(control.value === null || control.value.length == 0  || (control.value.match(/^[a-z]+$/)) || (control.pristine )){
//             return null;
//         }
//         return {'textonly': true};
//     }

// function emailMatcherValidation(firstControlName: string, secondControlName: string): ValidatorFn {
//     return (control:AbstractControl): {[key:string]:boolean} | null =>{

//         let firstEmail = control.get(firstControlName);
//         let secondEmail = control.get(secondControlName);
    
//         if((firstEmail.value === secondEmail.value) || (firstEmail.pristine || secondEmail.pristine)){
//             return null;
//         }
//         return {'match': true};
//     }
// }
// function emailMatcherValidation(control:AbstractControl): {[key:string]:boolean} | null {

//     let firstEmail = control.get("email");
//     let secondEmail = control.get("confirmEmail");

//     if((firstEmail.value === secondEmail.value) || (firstEmail.pristine || secondEmail.pristine)){
//         return null;
//     }
//     return {'match': true};
// }

//  function emailMatcherValidation(controlSecond:AbstractControl): ValidatorFn {
//   return (control:AbstractControl): {[key:string]:boolean} | null =>{

//     if(control.value != null) {    
//               if((controlSecond.value === control.value) || (controlSecond.pristine || control.pristine)){
//             return null;
//         }
//         return {'match': true};
//     }  
//     return null;
//   }
//}

//export{emailMatcherValidation, mobileValidation, textValidation};