import { Component, OnInit } from '@angular/core';
import { CarListData } from '../../../interfaces/car-list-data';
import { Router } from '@angular/router';
import { CarService } from '../../../services/car.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrl: './car-list.component.css'
})
export class CarListComponent implements OnInit {
  cars: CarListData[] = [];
  error: any;
  size: number = 30;
  currentPage: number = 1;
  searchText: string = '';
  carId!: number;
  carData: any = {};
  bodyTypeRange: number[] = [1, 2, 3, 4, 5, 6];
  fuelRange: number[] = [1, 2, 3, 4, 5];
  colorRange: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
  gearboxRange: number[] = [1, 2];
  propulsionRange: number[] = [1, 2, 3];

  constructor(private router: Router, private translate: TranslateService, private toastr: ToastrService, private carService: CarService) { }

  ngOnInit(): void {
    this.carService.getCars()
      .subscribe(
        (cars: CarListData[]) => {
          this.cars = cars;
        },
        (error: any) => {
          this.error = error;
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
  }

  navigateToCarDetails(id: number) {
    this.router.navigate(['/car', id]);
  }

  setEmptyFields() {
    this.carData = {};
  }

  setCarId(id: number) {
    this.carData = {};
    this.carId = id;
    this.carService.getCarById(id)
      .subscribe(
        (carData: any) => {
          this.carData = carData;
        },
        (error: any) => {
          this.error = error;
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
  }

  createCar() {
    this.carService.createCar(this.carData)
      .subscribe(
        () => {
          this.carService.getCars()
            .subscribe(
              (cars: CarListData[]) => {
                this.cars = cars;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.CREATED.CAR'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          this.error = error;
          this.toastr.error(this.translate.instant('ALERT.ERROR.CREATED.CAR'));
        }
      );
      this.carData = {};
  }

  updateCar() {
    this.carService.updateCar(this.carId, this.carData)
      .subscribe(
        () => {
          this.carService.getCars()
            .subscribe(
              (cars: CarListData[]) => {
                this.cars = cars;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.UPDATED.CAR'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          this.error = error;
          this.toastr.error(this.translate.instant('ALERT.ERROR.UPDATED.CAR'));
        }
      );
      this.carData = {};
  }

  deleteCar() {
    this.carService.deleteCar(this.carId)
      .subscribe(
        () => {
          this.carService.getCars()
            .subscribe(
              (cars: CarListData[]) => {
                this.cars = cars;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.DELETED.CAR'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DELETED.CAR'));
        }
      );
  }

  search(): CarListData[] {
    if (!this.searchText) {
      return this.cars;
    }
    return this.cars.filter(car =>
      car.fullName.toLowerCase().includes(this.searchText.toLowerCase())
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
