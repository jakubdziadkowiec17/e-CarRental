import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClientService } from '../../../services/client.service';
import { ClientData } from '../../../interfaces/client-data';
import { RentalData } from '../../../interfaces/rental-data';
import { RentalService } from '../../../services/rental.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrl: './client-details.component.css'
})
export class ClientDetailsComponent implements OnInit {
  size: number = 5;
  currentPage: number = 1;
  clientId!: number;
  clientData!: ClientData;
  rentals: RentalData[] = [];
  
  constructor(private route: ActivatedRoute, private translate: TranslateService, private toastr: ToastrService, private clientService: ClientService, private rentalService: RentalService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id !== null) {
        this.clientId = parseInt(id);
        this.clientService.getClientById(this.clientId).subscribe(
          data1 => {
            this.clientData = data1;
            this.rentalService.getRentalsByClientId(this.clientId).subscribe(
              (data: RentalData[]) => {
                this.rentals = data;
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

  totalPages(): number[] {
    const records = this.rentals;
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
