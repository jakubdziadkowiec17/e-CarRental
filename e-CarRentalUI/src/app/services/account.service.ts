import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { LoginData } from '../interfaces/login-data';
import { RegisterData } from '../interfaces/register-data';
import { Router } from '@angular/router';
import { ResetPasswordData } from '../interfaces/reset-password-data';
import { UserData } from '../interfaces/user-data';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private tokenKey = 'ASP.NET TOKEN';
  private userKey = 'USER';
  id!: number;
  email!: string;

  constructor(private http: HttpClient, private router: Router, private cookieService: CookieService) {}

  public login(data: LoginData): Observable<string> {
    return this.http.post<string>(
      `${environment.apiUrl}/Account/login`,
      data,
      { responseType: 'text' as 'json' }
    ).pipe(
      tap(token => {
        this.cookieService.set(this.tokenKey, token);
      })
    );
  }

  public isAdmin(): Observable<boolean> {
    if (typeof localStorage === 'undefined') {
        return of(false);
    }
    const currentUser = localStorage.getItem(this.userKey);
    if (!currentUser) {
      throw new Error('Empty User');
    }
    const user = JSON.parse(currentUser);
    if (!user.email) {
      throw new Error('Empty E-mail');
    }
    return this.http.get<boolean>(`${environment.apiUrl}/Account/isAdmin?email=${user.email}`);
}

getCurrentUser(email: string): Observable<UserData> {
  return this.http.get<UserData>(`${environment.apiUrl}/Account/getCurrentUser?email=${email}`)
    .pipe(
      tap(user => {
        localStorage.setItem(this.userKey, JSON.stringify({ id: user.id, email: user.email }));
      })
    );
}

  getCurrentUserSettings(): Observable<UserData> {
    const currentUser = localStorage.getItem(this.userKey);
    if (!currentUser) {
      throw new Error('Empty User');
    }
    const { id, email } = JSON.parse(currentUser);
    if (!email) {
      throw new Error('Empty E-mail');
    }
    return this.http.get<UserData>(`${environment.apiUrl}/Account/getCurrentUser?email=${email}`)
      .pipe(
        tap(user => {
          // Zapisz tylko id i email do localStorage
          localStorage.setItem(this.userKey, JSON.stringify({ id, email }));
        })
      );
  }

  public getCurrentUserEmail(): string {
    const currentUser = localStorage.getItem(this.userKey);
    if (!currentUser) {
      throw new Error('Empty User');
    }
    const user = JSON.parse(currentUser);
    const email = user.email;
    if (!email) {
      throw new Error('Empty E-mail');
    }
    return email;
  }

  editProfile(email: string, data: UserData): Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/Account/editProfile?email=${email}`, data, { responseType: 'text' as 'json' }
    ).pipe(
      tap(() => {
        this.getCurrentUser(data.email).subscribe();
      }),tap(response => {
        console.log(response);
      })
    );
  }

  resetPassword(email: string, data: ResetPasswordData): Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/Account/resetPassword?email=${email}`, data, { responseType: 'text' as 'json' }
    ).pipe(
      tap(response => {
        console.log(response);
      })
    );
  }

  public logout(): void {
    this.cookieService.delete(this.tokenKey);
    localStorage.removeItem(this.userKey);
    this.router.navigate(['/login']);
  }

  public isLoggedIn(): boolean {
    if (typeof localStorage !== 'undefined') {
      let token = this.cookieService.get(this.tokenKey);
      return token != null && token.length > 0;
    }
    return false;
  }

  public getToken(): string | null {
    return this.cookieService.get(this.tokenKey);
  }
}