import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ValidatorFn } from '../../common/validatorFn';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { LocationService } from '../../services/location.service';
import { Patient } from '../../models/patient';
import { ICity } from '../../models/city';
import { Subscription } from 'rxjs';
import { ValidationErrorMessenger } from '../../common/ValidationErrorMessenger';
import { City } from 'src/app/models/city';
import { State } from 'src/app/models/state';

@Component({
  selector: 'app-patient-edit',
  templateUrl: './patient-edit.component.html',
  styles: []
})
export class PatientEditComponent implements OnInit, OnDestroy{
  public errorMessage: any;
  public PatientForm: FormGroup;
  public pageTitle = 'Add new patient';
  public Patient: Patient | undefined;
  public Cities: City [] | undefined;
  public FilteredCities: City [] | undefined;
  public States: State [] | undefined;
  private sub: Subscription;
  private validationMessages: {[key:string]: {[key:string]: string }};
  public displayValidationMessages: {[key: string]:string} = {};
  public validationErrorMessenger: ValidationErrorMessenger;
  private PatientId:number = 0;
  private _selectedStateId: number = 1;
  
  constructor(private fb: FormBuilder, private route: ActivatedRoute,
    private router: Router,
    private PatientService: PatientService,
    private LocationService: LocationService) {

      this.validationMessages = {
        surname:{
          required: 'SurName is required.',
          text: 'Enter aphabhates only.'
        },
        name:{
          required: 'Patient name is required.',
          text: 'Enter aphabhates only.'
        },
        dob:{
          required: 'dob name is required.',
          Last100YearDate: 'Enter date in between last 100 years'
        },
        gender:{
          text: 'Enter aphabhates only.'
        },
        cityId:{
          number: 'Enter numbers only.'
        },
        stateId:{
          number: 'Enter numbers only.'
        }
      }

      this.validationErrorMessenger = new ValidationErrorMessenger(this.validationMessages);
     }

  ngOnInit() {
    this.setDefaultValues();
    this.getAllStates();
    this.getAllCities();
    
    this.sub = this.route.paramMap.subscribe(
      params => {
        this.PatientId = +params.get('Id');
      }
    );
      
    this.PatientForm.valueChanges.subscribe( value =>
      this.displayValidationMessages = this.validationErrorMessenger.getValidationErrorMessages(this.PatientForm)
    )

    this.PatientForm.get("stateId").valueChanges.subscribe(selectedValue  => {
      this.FilteredCities = selectedValue ? this.filterCities(selectedValue) : this.Cities;
      });
  
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  public setDefaultValues()
  {   
    this.PatientForm = this.fb.group({
      name: ['',[Validators.required]],
      surname: ['',[Validators.required]],
      gender:['M'],
      cityId:['', [ValidatorFn.number,]],
      stateId:['', [ValidatorFn.number]],
      dob:['']
    });

    this.PatientForm.get("name").setValidators(ValidatorFn.text);
    this.PatientForm.get("surname").setValidators(ValidatorFn.text);
    this.PatientForm.get("gender").setValidators(ValidatorFn.text);
    this.PatientForm.get("dob").setValidators(ValidatorFn.Last100YearDate);
  }

  getPatient(id: number) {
    this.PatientService.getPatientById(id).subscribe(
      Patient =>{
                  this.Patient = Patient;
                  if (id != 0 &&(this.Patient == null || this.Patient.Id == 0 && this.PatientForm)) {
                    this.errorMessage = `Unable to find Patient with id =${id}.`;
                  }else if(id!= 0 &&(this.Patient != null || this.Patient.Id != 0 && this.PatientForm)){
                    this.displayPatient();
                    this.pageTitle = `Edit Patient ${this.Patient.Name}`;
                  }
                  else{
                    //this.Patient = new Patient(0,'','',new Date(),'',0);
                    this.Patient = undefined;
                    this.pageTitle = `Add new Patient`;
                  }
      },
      error => this.errorMessage = <any>error);
  }

  getAllCities() {
    this.LocationService.getAllCities().subscribe(
      cities =>{
                  this.Cities = cities;
                  if (this.Cities == null || this.Cities == undefined) {
                    this.errorMessage = `Unable fetch all cities`;
                  }else if(this.Cities != undefined && this.Cities != null){
                    //this.displayPatient();
                  }
      },
      error => this.errorMessage = <any>error);
  }

  getAllStates() {
    this.LocationService.getAllState().subscribe(
      states =>{
                  this.States = states;
                  if (this.States == null || this.States == undefined) {
                    this.errorMessage = `Unable fetch all States`;
                  }else if(this.States != undefined && this.States != null){
                    //this.displayPatient();
                  }
      },
      error => this.errorMessage = <any>error);
  }
  
  filterCities(stateId: number): ICity[] {
    return this.Cities.filter((city : ICity) =>
    city.stateId == stateId );
  }

  displayPatient(){
    this.PatientForm.patchValue({
      name: this.Patient.Name,
      surname: this.Patient.SurName,
      gender: this.Patient.Gender,
      dob: this.Patient.DOB,
      cityId: this.Patient.CityId,
      stateId: 0,
    })
  }

  public save(){
    if(this.PatientForm.dirty){
      if(this.PatientForm.valid){
        const Patient = {...this.Patient, ...this.PatientForm.value}
        if(this.PatientId == 0){      
          this.PatientService.addPatient(Patient).subscribe(
            response =>{alert('Patient addedd successfully');
                        this.completeAction();},
            error=> this.errorMessage == JSON.stringify(error))
        }else{
          // this.PatientService.updatePatient(Patient).subscribe(
          //   response =>{alert('Patient updated successfully');
          //               this.completeAction();},
          //   error=> this.errorMessage == JSON.stringify(error))
        }      
      }else{
        alert('Resolve all the errors before saving Patient.'); 
      }

    }else{
      alert('No changes maid to Patient.')
    }
  }

  private completeAction(){
    this.PatientForm.reset();
    this.router.navigate(['/patients']);
  }

  reset(){
    this.PatientForm.reset();
  }

  cancel(){
    this.completeAction();
  }
}
