import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { map, Observable } from 'rxjs';

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

  isActive(route: string): boolean {
    return this.router.url === '/' + route;
  }

  isAdmin(): Observable<boolean> {
    return this.accountService.isAdmin().pipe(
      map(isAdmin => {
        return isAdmin;
      })
    );
  }
}
