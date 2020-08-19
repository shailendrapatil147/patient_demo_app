import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { PatientListComponent } from './patient-list.component';
import { SharedModule } from "../../shared/shared.module";
import { PatientRoutingModule } from './patient-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { PatientEditComponent } from './patient-edit.component';


@NgModule({
  declarations: [
    PatientListComponent,
    PatientEditComponent],

  imports: [
    SharedModule,
    PatientRoutingModule, 
    ReactiveFormsModule]
})
export class PatientModule { }
