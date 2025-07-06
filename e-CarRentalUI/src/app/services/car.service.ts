import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { CarListData } from '../interfaces/car-list-data';
import { CarData } from '../interfaces/car-data';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  constructor(private http: HttpClient) { }

  getCarCount(): Observable<number> {
    return this.http.get<number>(environment.apiUrl + '/Car/count');
  }

  getCars(): Observable<CarListData[]> {
    return this.http.get<CarListData[]>(environment.apiUrl + '/Car');
  }

  getCarById(id: number): Observable<CarData> {
    return this.http.get<CarData>(environment.apiUrl + `/Car/${id}`);
  }

  createCar(data: CarData): Observable<any> {
    return this.http.post(environment.apiUrl + '/Car', data);
  }

  updateCar(id: number, data: CarData): Observable<any> {
    return this.http.put(environment.apiUrl + `/Car/${id}`, data);
  }

  deleteCar(id: number): Observable<any> {
    return this.http.delete(environment.apiUrl + `/Car/${id}`);
  }
}