import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { IPatient, Patient } from '../models/patient';
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { ICity } from '../models/city';
import { IState } from '../models/state';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private apiBaseUrl = `https://localhost:50010/`;

  constructor(private _httpService: HttpClient) { }

  getAllCities(): Observable<ICity[]>{
    return this._httpService.get<ICity[]>(`${this.apiBaseUrl }location/GetAllCities`).pipe(
        catchError(this.errorHandler));
  }

  getAllState(): Observable<IState[]>{
    return this._httpService.get<IState[]>(`${this.apiBaseUrl }location/GetAllStates`).pipe(
      //.tap((response: Response) => <IPatient>response.json()),
      catchError(this.errorHandler));
  }

  private errorHandler(response: HttpErrorResponse){
      if(response instanceof ErrorEvent)
      {
      }else{
      } 
      let errorMessage = `Server returned code: ${response.status}, error message is: ${response.message}`;
      return throwError(errorMessage);
  }
}
