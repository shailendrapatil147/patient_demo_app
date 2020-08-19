import { FormGroup } from "@angular/forms";

export class ValidationErrorMessenger {

    constructor(private validationMessages:{[key:string]: {[key:string]: string}}){

    }

    getValidationErrorMessages(form: FormGroup){
        const messages ={};
        for (const control in form.controls){
            if(form.controls.hasOwnProperty(control)){
                const controlObj = form.controls[control];

                if(controlObj instanceof FormGroup){
                    const childVAlidationMessages = this.getValidationErrorMessages(controlObj);
                    Object.assign(messages, childVAlidationMessages);
                }else{
                    if(this.validationMessages[control]){
                        messages[control] = '';
                        if((controlObj.dirty || controlObj.touched) && controlObj.errors){
                            Object.keys(controlObj.errors).map(messageName=> {
                                if(this.validationMessages[control][messageName]){
                                    messages[control] += this.validationMessages[control][messageName] + '';
                                }
                            })
                        }
                    }
                }
            }
        }
        return messages;
    }
}
