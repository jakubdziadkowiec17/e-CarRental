import { Component, OnInit } from '@angular/core';
import { PaymentData } from '../../../interfaces/payment-data';
import { Router } from '@angular/router';
import { PaymentService } from '../../../services/payment.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { map, Observable } from 'rxjs';
import { AccountService } from '../../../services/account.service';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrl: './payment-list.component.css'
})

export class PaymentListComponent implements OnInit {
  isAdmin$: Observable<boolean>;
  private userKey = 'USER';
  payments: PaymentData[] = [];
  error: any;
  size: number = 30;
  currentPage: number = 1;
  searchText: string = '';
  paymentId!: number;
  paymentData: any = {};

  constructor(private router: Router, private translate: TranslateService, private accountService: AccountService, private toastr: ToastrService, private paymentService: PaymentService) {
    this.isAdmin$ = this.isAdmin();
  }

  ngOnInit(): void {
    this.paymentService.getPayments()
      .subscribe(
        (data: PaymentData[]) => {
          this.payments = data;
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
    this.paymentData = {};
  }

  setPaymentId(id: number) {
    this.paymentData = {};
    this.paymentId = id;
    this.paymentService.getPaymentById(id)
      .subscribe(
        (PaymentData: any) => {
          this.paymentData = PaymentData;
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
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
          this.paymentService.getPayments()
            .subscribe(
              (data: PaymentData[]) => {
                this.payments = data;
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

  deletePayment() {
    this.paymentService.deletePayment(this.paymentId)
      .subscribe(
        () => {
          this.paymentService.getPayments()
            .subscribe(
              (data: PaymentData[]) => {
                this.payments = data;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.DELETED.PAYMENT'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DELETED.PAYMENT'));
        }
      );
  }

  search(): PaymentData[] {
    if (!this.searchText) {
      return this.payments;
    }
    return this.payments.filter(payment =>
      payment.id.toString().toLowerCase().includes(this.searchText.toLowerCase())
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

  isAdmin(): Observable<boolean> {
    return this.accountService.isAdmin().pipe(
      map(isAdmin => {
        return isAdmin;
      })
    );
  }
}
