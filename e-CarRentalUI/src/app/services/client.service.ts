import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { ClientListData } from '../interfaces/client-list-data';
import { ClientData } from '../interfaces/client-data';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  constructor(private http: HttpClient) { }

  getClientCount(): Observable<number> {
    return this.http.get<number>(environment.apiUrl + '/Client/count');
  }

  getClients(): Observable<ClientListData[]> {
    return this.http.get<ClientListData[]>(environment.apiUrl + '/Client');
  }

  getClientById(id: number): Observable<ClientData> {
    return this.http.get<ClientData>(environment.apiUrl + `/Client/${id}`);
  }

  createClient(data: ClientData): Observable<any> {
    return this.http.post(environment.apiUrl + '/Client', data);
  }

  updateClient(id: number, data: ClientData): Observable<any> {
    return this.http.put(environment.apiUrl + `/Client/${id}`, data);
  }

  deleteClient(id: number): Observable<any> {
    return this.http.delete(environment.apiUrl + `/Client/${id}`);
  }
}