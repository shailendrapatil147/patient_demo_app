import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PatientListComponent } from './patient-list.component';
import { PatientEditComponent } from './patient-edit.component';
import { PatientEditGuard } from './patient-edit.guard';

@NgModule({
  imports: [ RouterModule.forChild([
    {path: 'patients', component: PatientListComponent},
    {path: 'patient/:PatientId/edit', canDeactivate:[PatientEditGuard], component: PatientEditComponent},
    {path: 'patientdetails/:PatientId/edit', canDeactivate:[PatientEditGuard], component: PatientEditComponent},
    {path: 'addpatient', canDeactivate:[PatientEditGuard], component: PatientEditComponent}
  ])
  ],

  exports: [RouterModule]
})

export class PatientRoutingModule { }
