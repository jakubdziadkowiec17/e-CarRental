import { Component, OnInit } from '@angular/core';
import { RentalData } from '../../../interfaces/rental-data';
import { RentalService } from '../../../services/rental.service';
import { Router } from '@angular/router';
import { CarData } from '../../../interfaces/car-data';
import { CarListData } from '../../../interfaces/car-list-data';
import { CarService } from '../../../services/car.service';
import { ClientListData } from '../../../interfaces/client-list-data';
import { ClientService } from '../../../services/client.service';
import { PaymentService } from '../../../services/payment.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-rental-list',
  templateUrl: './rental-list.component.html',
  styleUrl: './rental-list.component.css'
})
export class RentalListComponent implements OnInit {
  private userKey = 'USER';
  rentals: RentalData[] = [];
  error: any;
  size: number = 30;
  currentPage: number = 1;
  searchText: string = '';
  rentalId!: number;
  rentalData: any = {};
  cars: CarListData[] = [];
  clients: ClientListData[] = [];
  paymentData: any = {};

  constructor(private router: Router, private translate: TranslateService, private toastr: ToastrService, private rentalService: RentalService, private carService: CarService, private clientService: ClientService, private paymentService: PaymentService) { }

  ngOnInit(): void {
    this.rentalService.getRentals()
      .subscribe(
        (data: RentalData[]) => {
          this.rentals = data;
        },
        (error: any) => {
          this.error = error;
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
  }

  navigateToRentalDetails(id: number) {
    this.router.navigate(['/rental', id]);
  }

  setEmptyFields() {
    this.rentalData = {};
    this.cars = [];
    this.clients = [];
    this.carService.getCars().subscribe(
      (data: CarListData[]) => {
        this.cars = data;
      },
      (error: any) => {
        this.error = error;
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
    this.clientService.getClients().subscribe(
      (data: ClientListData[]) => {
        this.clients = data;
      },
      (error: any) => {
        this.error = error;
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
  }

  setPaymentId(id: number) {
    this.rentalId = id;
    this.paymentData.rentalId = this.rentalId;
  }

  setRentalId(id: number) {
    this.rentalData = {};
    this.rentalId = id;
    this.cars = [];
    this.clients = [];
    this.carService.getCars().subscribe(
      (data: CarListData[]) => {
        this.cars = data;
      },
      (error: any) => {
        this.error = error;
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
    this.clientService.getClients().subscribe(
      (data: ClientListData[]) => {
        this.clients = data;
      },
      (error: any) => {
        this.error = error;
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
    this.rentalService.getRentalById(id)
      .subscribe(
        (data: any) => {
          this.rentalData = data;
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
  }

  createRental() {
    const currentUser = localStorage.getItem(this.userKey);
    if (!currentUser) {
      throw new Error('Empty User');
    }
    const user = JSON.parse(currentUser);
    this.rentalData.userId = user.id;
    this.rentalService.createRental(this.rentalData)
      .subscribe(
        () => {
          this.rentalService.getRentals()
            .subscribe(
              (Rentals: RentalData[]) => {
                this.rentals = Rentals;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.CREATED.RENTAL'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.CREATED.RENTAL'));
        }
      );
      this.rentalData = {};
      this.cars = [];
      this.clients = [];
  }

  createPayment() {
    const currentUser = localStorage.getItem(this.userKey);
    if (!currentUser) {
      throw new Error('Empty User');
    }
    const user = JSON.parse(currentUser);
    this.paymentData.userId = user.id;
    this.paymentService.createPayment(this.paymentData)
      .subscribe(
        () => {
          this.rentalService.getRentals()
            .subscribe(
              (Rentals: RentalData[]) => {
                this.rentals = Rentals;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.CREATED.PAYMENT'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.CREATED.PAYMENT'));
        }
      );
      this.paymentData = {};
  }

  updateRental() {
    this.rentalService.updateRental(this.rentalId, this.rentalData)
      .subscribe(
        () => {
          this.rentalService.getRentals()
            .subscribe(
              (data: RentalData[]) => {
                this.rentals = data;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.UPDATED.RENTAL'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.UPDATED.RENTAL'));
        }
      );
      this.rentalData = {};
      this.cars = [];
      this.clients = [];
  }

  deleteRental() {
    this.rentalService.deleteRental(this.rentalId)
      .subscribe(
        () => {
          this.rentalService.getRentals()
            .subscribe(
              (data: RentalData[]) => {
                this.rentals = data;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.DELETED.RENTAL'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DELETED.RENTAL'));
        }
      );
  }

  search(): RentalData[] {
    if (!this.searchText) {
      return this.rentals;
    }
    return this.rentals.filter(rental =>
      rental.id.toString().toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  totalPages(): number[] {
    const records = this.search();
    return Array(Math.ceil(records.length / this.size)).fill(0).map((x, i) => i + 1);
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  setCurrentPage(page: number) {
    this.currentPage = page;
  }

  nextPage() {
    if (this.currentPage < this.totalPages().length) {
      this.currentPage++;
    }
  }
}
