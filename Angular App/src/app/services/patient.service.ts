import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { IPatient, Patient } from '../models/patient';
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private patientUrl = `https://localhost:50010/`;

  constructor(private _httpService: HttpClient) { }

  getAllPatients(): Observable<IPatient[]>{
    return this._httpService.get<IPatient[]>(`${this.patientUrl}patient/getallpatient`).pipe(
        //tap(data=> console.log(`Response: ${JSON.stringify(data)}`)),
        catchError(this.errorHandler));
  }

  getPatientById(patientId: number): Observable<IPatient>{
    return this._httpService.get<IPatient>(`${this.patientUrl}/getpatientById?Id=${patientId}`).pipe(
      //.tap((response: Response) => <IPatient>response.json()),
      catchError(this.errorHandler));
  }

  addPatient(Patient: Patient): Observable<IPatient>{    
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });    
    return this._httpService.post<IPatient>(`${this.patientUrl}patient/SavePatient`, Patient, { headers: headers })
      .pipe(
        tap(data => console.log('createPatient: ' + JSON.stringify(data))),
        catchError(this.errorHandler)
      );
  }

  // updatePatient(Patient: IPatient): Observable<IPatient>{
  //   const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //   return this._httpService.put<IPatient>(`${this.PatientsUrl}/updatePatientbyidasync`,Patient, {headers: headers})
  //   .pipe(
  //     tap(response => console.log(JSON.stringify(response))),
  //     catchError(this.errorHandler));
  // }

  private errorHandler(response: HttpErrorResponse){
      if(response instanceof ErrorEvent)
      {
      }else{
      } 
      let errorMessage = `Server returned code: ${response.status}, error message is: ${response.message}`;
      return throwError(errorMessage);
      //throw response.error;
  }
  
}
