import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { SettingsComponent } from './components/settings/settings.component';
import { ClientListComponent } from './components/client/client-list/client-list.component';
import { ClientDetailsComponent } from './components/client/client-details/client-details.component';
import { EmployeeListComponent } from './components/employee/employee-list/employee-list.component';
import { CarListComponent } from './components/car/car-list/car-list.component';
import { CarDetailsComponent } from './components/car/car-details/car-details.component';
import { RentalListComponent } from './components/rental/rental-list/rental-list.component';
import { RentalDetailsComponent } from './components/rental/rental-details/rental-details.component';
import { PaymentListComponent } from './components/payment/payment-list/payment-list.component';
import { AdminGuard } from './helpers/admin.guard';
import { LoginGuard } from './helpers/login.guard';

const routes: Routes = [
  {
  path: '',
  component: HomeComponent,
  canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [LoginGuard]
  },
  {
    path: 'rentals',
    component: RentalListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'rental/:id',
    component: RentalDetailsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'cars',
    component: CarListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'car/:id',
    component: CarDetailsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'clients',
    component: ClientListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'client/:id',
    component: ClientDetailsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'employees',
    component: EmployeeListComponent,
    canActivate: [AuthGuard, AdminGuard]
  },
  {
    path: 'payments',
    component: PaymentListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'settings',
    component: SettingsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
