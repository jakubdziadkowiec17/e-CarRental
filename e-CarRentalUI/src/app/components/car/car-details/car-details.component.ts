import { Component, OnInit } from '@angular/core';
import { RentalData } from '../../../interfaces/rental-data';
import { CarData } from '../../../interfaces/car-data';
import { ActivatedRoute } from '@angular/router';
import { RentalService } from '../../../services/rental.service';
import { CarService } from '../../../services/car.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-car-details',
  templateUrl: './car-details.component.html',
  styleUrl: './car-details.component.css'
})
export class CarDetailsComponent implements OnInit {
  size: number = 15;
  currentPage: number = 1;
  carId!: number;
  carData!: CarData;
  rentals: RentalData[] = [];
  
  constructor(private route: ActivatedRoute, private translate: TranslateService, private toastr: ToastrService, private carService: CarService, private rentalService: RentalService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id !== null) {
        this.carId = parseInt(id);
        this.carService.getCarById(this.carId).subscribe(
          data1 => {
            this.carData = data1;
            this.rentalService.getRentalsByCarId(this.carId).subscribe(
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