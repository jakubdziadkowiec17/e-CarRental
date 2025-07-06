import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { map, Observable } from 'rxjs';
import { Role } from '../../Constants/Role';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent{
  isAdmin$: Observable<boolean>;

  constructor(private router: Router, private accountService: AccountService) {
    this.isAdmin$ = this.isAdmin();
   }

  handleNavigate(route: string) {
    this.router.navigate([route]);
  }

  logout(): void {
    this.accountService.logout();
  }

  isHomeActive(route: string): boolean {
    return this.router.url === '/' + route;
  }

  isActive(route: string): boolean {
    const url = this.router.url;
    return url.includes(route) || url.includes(route);
  }

  isAdmin(): Observable<boolean> {
    return this.accountService.hasAccessToRoles([Role.Admin]).pipe(
      map(isAdmin => {
        return isAdmin;
      })
    );
  }
}
