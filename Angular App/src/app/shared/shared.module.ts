import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { StarComponent } from '../components/star/star.component';
import { ConverToSpacesPipe } from '../pipes/conver-to-spaces.pipe';
import { EmailValidatorDirective } from '../common/email-validator.directive';

@NgModule({
  declarations: [
    StarComponent,
    ConverToSpacesPipe,
    EmailValidatorDirective
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    StarComponent,
    ConverToSpacesPipe
  ]
})

export class SharedModule { }
