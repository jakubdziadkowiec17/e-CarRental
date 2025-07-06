import { Component, HostListener, OnInit } from '@angular/core';
import { RentalService } from '../../services/rental.service';
import { CarService } from '../../services/car.service';
import { EmployeeService } from '../../services/employee.service';
import { ClientService } from '../../services/client.service';
import { PaymentService } from '../../services/payment.service';
import { ChartData } from '../../interfaces/chart-data';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { Role } from '../../Constants/Role';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isAdmin$: Observable<boolean>;
  rentalCount: number = 0;
  carCount: number = 0;
  employeeCount: number = 0;
  clientCount: number = 0;
  paymentCount: number = 0;
  rentalMonthCount: { name: string; value: number; }[] = [];

  screenWidth: number = window.innerWidth;
  chartsContainerWidth: number = 0.755 * this.screenWidth;

  constructor(private router: Router,
    private rentalService: RentalService,
    private carService: CarService,
    private clientService: ClientService,
    private employeeService: EmployeeService,
    private paymentService: PaymentService, private accountService: AccountService, private translate: TranslateService, private toastr: ToastrService
  ) {
    this.isAdmin$ = this.isAdmin();
   }

  ngOnInit(): void {
    this.getRentalCount();
    this.getCarCount();
    this.getClientCount();
    this.getPaymentCount();
    this.getRentalMonthCount();

    this.isAdmin$.subscribe(isAdmin => {
      if (isAdmin) {
        this.getEmployeeCount();
      }
    });
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.screenWidth = event.target.innerWidth;
    this.chartsContainerWidth = 0.755 * this.screenWidth;
  }

  handleNavigate(route: string) {
    this.router.navigate([route]);
  }

  getRentalCount() {
    this.rentalService.getRentalCount().subscribe(
      (count) => {
        this.rentalCount = count;
      },
      (error) => {
        console.error('Error', error);
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
  }

  getCarCount() {
    this.carService.getCarCount().subscribe(
      (count) => {
        this.carCount = count;
      },
      (error) => {
        console.error('Error', error);
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
  }

  getEmployeeCount() {
    this.employeeService.getEmployeeCount().subscribe(
      (count) => {
        this.employeeCount = count;
      },
      (error) => {
        console.error('Error', error);
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
  }

  getClientCount() {
    this.clientService.getClientCount().subscribe(
      (count) => {
        this.clientCount = count;
      },
      (error) => {
        console.error('Error', error);
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
  }

  getPaymentCount() {
    this.paymentService.getPaymentCount().subscribe(
      (count) => {
        this.paymentCount = count;
      },
      (error) => {
        console.error('Error', error);
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
  }

  getRentalMonthCount() {
    this.rentalService.getRentalMonthCount().subscribe(
      (count: ChartData[]) => {
        this.rentalMonthCount = count.map(item => {
          const [month, year] = item.monthWithYear.split('.');
          const monthName = this.translate.instant(`HOME.CHARTMONTH.${Number(month)}`);
          return {
            name: `${monthName} ${year}`,
            value: item.monthCount
          };
        });
      },
      (error) => {
        console.error('Error', error);
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
  }

  isAdmin(): Observable<boolean> {
    return this.accountService.hasAccessToRoles([Role.Admin]).pipe(
      map(isAdmin => {
        return isAdmin;
      })
    );
  }
}