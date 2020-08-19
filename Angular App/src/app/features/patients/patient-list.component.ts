import { Component, OnInit, ReflectiveInjector } from '@angular/core';
import {IPatient} from '../../models/patient';
import { PatientService } from '../../services/patient.service';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  pageTitle: String = 'Patient List';
  _errorMessage;
  get ErrorMessage():string{
    return this._errorMessage;
  }
  set ErrorMessage(value: string){
    this._errorMessage = value;
  }

  Patients: IPatient[] = null;
  filteredPatients: IPatient[] = null;

  constructor(private _PatientService: PatientService) { }

  ngOnInit() {
    this._PatientService.getAllPatients().subscribe(
      Patients => {this.Patients = Patients;
                  this.filteredPatients = this.Patients},
      error => {this._errorMessage = <any>error;},
      ()=> {this.filteredPatients = this.Patients;}
    );
  }

}
