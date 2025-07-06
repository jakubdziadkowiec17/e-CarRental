import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { UserData } from '../interfaces/user-data';
import { EmployeeListData } from '../interfaces/employee-list-data';
import { RegisterData } from '../interfaces/register-data';
import { ResetPasswordAdminData } from '../interfaces/reset-password-admin-data';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  constructor(private http: HttpClient) { }

  getEmployeeCount(): Observable<number> {
    return this.http.get<number>(environment.apiUrl + '/Employee/count');
  }

  getEmployees(): Observable<EmployeeListData[]> {
    return this.http.get<EmployeeListData[]>(environment.apiUrl + '/Employee');
  }

  getEmployeeById(id: string): Observable<UserData> {
    return this.http.get<UserData>(environment.apiUrl + `/Employee/${id}`);
  }

  createEmployee(data: RegisterData): Observable<any> {
    return this.http.post(environment.apiUrl + '/Employee', data);
  }

  updateEmployee(id: string, data: UserData): Observable<any> {
    return this.http.put(environment.apiUrl + `/Employee/${id}`, data);
  }

  deleteEmployee(id: string): Observable<any> {
    return this.http.delete(environment.apiUrl + `/Employee/${id}`);
  }

  resetPasswordForEmployee(id: string, data: ResetPasswordAdminData): Observable<any> {
    return this.http.put<any>(`${environment.apiUrl}/Employee/resetPasswordForEmployee?id=${id}`, data);
  }
}