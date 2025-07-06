import { Component, OnInit } from '@angular/core';
import { RentalData } from '../../../interfaces/rental-data';
import { PaymentData } from '../../../interfaces/payment-data';
import { ActivatedRoute, Router } from '@angular/router';
import { RentalService } from '../../../services/rental.service';
import { PaymentService } from '../../../services/payment.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-rental-details',
  templateUrl: './rental-details.component.html',
  styleUrl: './rental-details.component.css'
})
export class RentalDetailsComponent implements OnInit {
  size: number = 10;
  currentPage: number = 1;
  rentalId!: number;
  rentalData!: RentalData;
  payments: PaymentData[] = [];
  
  constructor(private route: ActivatedRoute, private translate: TranslateService, private toastr: ToastrService, private router: Router, private rentalService: RentalService, private paymentService: PaymentService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id !== null) {
        this.rentalId = parseInt(id);
        this.rentalService.getRentalById(this.rentalId).subscribe(
          data1 => {
            this.rentalData = data1;
            this.paymentService.getPaymentsByRentalId(this.rentalId).subscribe(
              (data: PaymentData[]) => {
                this.payments = data;
              },
              error => {
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
          },
          error => {
            this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
          }
        );
      }
    });
  }

  navigateToCarDetails(id: number) {
    this.router.navigate(['/car', id]);
  }

  navigateToClientDetails(id: number) {
    this.router.navigate(['/client', id]);
  }

  totalPages(): number[] {
    const records = this.payments;
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