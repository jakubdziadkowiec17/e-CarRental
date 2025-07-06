import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { PaymentData } from '../interfaces/payment-data';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  constructor(private http: HttpClient) { }

  getPaymentCount(): Observable<number> {
    return this.http.get<number>(environment.apiUrl + '/Payment/count');
  }

  getPayments(): Observable<PaymentData[]> {
    return this.http.get<PaymentData[]>(environment.apiUrl + '/Payment');
  }

  getPaymentsByRentalId(rentalId: number): Observable<PaymentData[]> {
    return this.http.get<PaymentData[]>(environment.apiUrl + `/Payment/getPaymentsByRentalId/${rentalId}`);
  }

  getPaymentById(id: number): Observable<PaymentData> {
    return this.http.get<PaymentData>(environment.apiUrl + `/Payment/${id}`);
  }

  createPayment(data: PaymentData): Observable<any> {
    return this.http.post(environment.apiUrl + '/Payment', data);
  }

  deletePayment(id: number): Observable<any> {
    return this.http.delete(environment.apiUrl + `/Payment/${id}`);
  }
}