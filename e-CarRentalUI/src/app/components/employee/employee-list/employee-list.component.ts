import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeService } from '../../../services/employee.service';
import { EmployeeListData } from '../../../interfaces/employee-list-data';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent implements OnInit {
  employees: EmployeeListData[] = [];
  error: any;
  size: number = 30;
  currentPage: number = 1;
  searchText: string = '';
  employeeId!: string;
  employeeData: any = {};
  registerData: any = {};
  resetPasswordData: any = {};

  constructor(private router: Router, private translate: TranslateService, private toastr: ToastrService, private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.employeeService.getEmployees()
      .subscribe(
        (employees: EmployeeListData[]) => {
          this.employees = employees;
        },
        (error: any) => {
          this.error = error;
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
  }

  navigateToEmployeeDetails(id: string) {
    this.router.navigate(['/employee', id]);
  }

  setEmptyFields() {
    this.registerData = {};
  }

  setClientId(id: string) {
    this.employeeData = {};
    this.resetPasswordData = {};
    this.employeeId = id;
    this.employeeService.getEmployeeById(id)
      .subscribe(
        (employeeData: any) => {
          this.employeeData = employeeData;
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
  }

  createEmployee() {
    this.employeeService.createEmployee(this.registerData)
      .subscribe(
        () => {
          this.employeeService.getEmployees()
            .subscribe(
              (employees: EmployeeListData[]) => {
                this.employees = employees;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.CREATED.EMPLOYEE'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.CREATED.EMPLOYEE'));
        }
      );
      this.registerData = {};
  }

  updateEmployee() {
    this.employeeService.updateEmployee(this.employeeId, this.employeeData)
      .subscribe(
        () => {
          this.employeeService.getEmployees()
            .subscribe(
              (employees: EmployeeListData[]) => {
                this.employees = employees;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.UPDATED.EMPLOYEE'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.UPDATED.EMPLOYEE'));
        }
      );
      this.employeeData = {};
  }

  deleteEmployee() {
    this.employeeService.deleteEmployee(this.employeeId)
      .subscribe(
        () => {
          this.employeeService.getEmployees()
            .subscribe(
              (employees: EmployeeListData[]) => {
                this.employees = employees;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.DELETED.EMPLOYEE'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DELETED.EMPLOYEE'));
        }
      );
  }

  resetPasswordForEmployee() {
    this.employeeService.resetPasswordForEmployee(this.employeeId, this.resetPasswordData)
      .subscribe(
        () => {
          this.employeeService.getEmployees()
            .subscribe(
              (data: EmployeeListData[]) => {
                this.employees = data;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.RESETPASSWORD.EMPLOYEE'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.RESETPASSWORD.EMPLOYEE'));
        }
      );
      this.resetPasswordData = {};
  }

  search(): EmployeeListData[] {
    if (!this.searchText) {
      return this.employees;
    }
    return this.employees.filter(employee =>
      employee.fullName.toLowerCase().includes(this.searchText.toLowerCase())
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
