import { Injectable } from '@angular/core';
import { Router, CanDeactivate } from '@angular/router';
import { PatientEditComponent } from './patient-edit.component';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientEditGuard implements  CanDeactivate<PatientEditComponent> {
    
    canDeactivate(
      component: PatientEditComponent
      ): Observable<boolean> | Promise<boolean> | boolean {
        if (component.PatientForm.dirty) {
        return confirm("Do you really wan to discard changes?");
        }
        return true;
    }
  }
