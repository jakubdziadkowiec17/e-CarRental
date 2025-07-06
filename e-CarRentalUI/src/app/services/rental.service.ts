import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { ChartData } from '../interfaces/chart-data';
import { RentalData } from '../interfaces/rental-data';

@Injectable({
  providedIn: 'root'
})
export class RentalService {
  constructor(private http: HttpClient) { }

  getRentalCount(): Observable<number> {
    return this.http.get<number>(environment.apiUrl + '/Rental/count');
  }

  getRentalMonthCount(): Observable<ChartData[]> {
    return this.http.get<ChartData[]>(environment.apiUrl + '/Rental/monthCount');
  }
  
  getRentals(): Observable<RentalData[]> {
    return this.http.get<RentalData[]>(environment.apiUrl + '/Rental');
  }

  getRentalsByClientId(clientId: number): Observable<RentalData[]> {
    return this.http.get<RentalData[]>(environment.apiUrl + `/Rental/getRentalsByClientId/${clientId}`);
  }

  getRentalsByCarId(carId: number): Observable<RentalData[]> {
    return this.http.get<RentalData[]>(environment.apiUrl + `/Rental/getRentalsByCarId/${carId}`);
  }

  getRentalById(id: number): Observable<RentalData> {
    return this.http.get<RentalData>(environment.apiUrl + `/Rental/${id}`);
  }

  createRental(data: RentalData): Observable<any> {
    return this.http.post(environment.apiUrl + '/Rental', data);
  }

  updateRental(id: number, data: RentalData): Observable<any> {
    return this.http.put(environment.apiUrl + `/Rental/${id}`, data);
  }

  deleteRental(id: number): Observable<any> {
    return this.http.delete(environment.apiUrl + `/Rental/${id}`);
  }
}