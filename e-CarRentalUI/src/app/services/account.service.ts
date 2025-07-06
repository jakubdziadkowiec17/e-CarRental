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
import { Claims } from '../Constants/Claims';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private tokenKey = 'ASP.NET TOKEN';
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

  getCurrentUser(email: string): Observable<UserData> {
    return this.http.get<UserData>(`${environment.apiUrl}/Account/getCurrentUser?email=${email}`);
  }

  getCurrentUserSettings(): Observable<UserData> {
    const userId = this.getUserId();
    return this.http.get<UserData>(`${environment.apiUrl}/Account/getCurrentUser?userId=${userId}`);
  }

  editProfile(data: UserData): Observable<any> {
    const userId = this.getUserId();
    return this.http.put<any>(
      `${environment.apiUrl}/Account/editProfile?userId=${userId}`, data, { responseType: 'text' as 'json' }
    ).pipe(
      tap(() => {
        this.getCurrentUser(data.email).subscribe();
      }),tap(response => {
        console.log(response);
      })
    );
  }

  resetPassword(data: ResetPasswordData): Observable<any> {
    const userId = this.getUserId();
    return this.http.put<any>(
      `${environment.apiUrl}/Account/resetPassword?userId=${userId}`, data, { responseType: 'text' as 'json' }
    ).pipe(
      tap(response => {
        console.log(response);
      })
    );
  }

  public logout(): void {
    this.cookieService.delete(this.tokenKey);
    this.router.navigate(['/login']);
  }

  public getToken(): string | null {
    return this.cookieService.get(this.tokenKey);
  }

  public hasAccessToRoles(roles: string[]): Observable<boolean> {
    const accessToken = this.getToken();

    if (!accessToken) {
      return of(false);
    }

    try {
      const decodedToken = jwtDecode<any>(accessToken);
      const rolesFromJWT = decodedToken[Claims.Roles];

      if (!rolesFromJWT) {
        return of(false);
      }

      const userRoles = Array.isArray(rolesFromJWT) ? rolesFromJWT : [rolesFromJWT];
      const hasAccess = roles.some(role => userRoles.includes(role));

      return of(hasAccess);
    } 
    catch (error) {
      return of(false);
    }
  }

  public getUserId(): string | null {
    const accessToken = this.getToken();

    if (!accessToken) {
      return null;
    }

    try {
      const decodedToken = jwtDecode<any>(accessToken);
      return decodedToken[Claims.UserId];
    } 
    catch (error) {
      return null;
    }
  }

  public isLoggedIn(): boolean {
    if (typeof localStorage !== 'undefined') {
      let token = this.cookieService.get(this.tokenKey);
      return token != null && token.length > 0;
    }
    return false;
  }
}