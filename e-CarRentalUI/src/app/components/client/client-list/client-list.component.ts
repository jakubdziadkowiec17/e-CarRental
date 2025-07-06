import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ClientListData } from '../../../interfaces/client-list-data';
import { ClientService } from '../../../services/client.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {
  clients: ClientListData[] = [];
  error: any;
  size: number = 20;
  currentPage: number = 1;
  searchText: string = '';
  clientId!: number;
  clientData: any = {};

  constructor(private router: Router, private translate: TranslateService, private toastr: ToastrService, private clientService: ClientService) { }

  ngOnInit(): void {
    this.clientService.getClients()
      .subscribe(
        (clients: ClientListData[]) => {
          this.clients = clients;
        },
        (error: any) => {
          this.error = error;
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
  }

  navigateToClientDetails(id: number) {
    this.router.navigate(['/client', id]);
  }

  setEmptyFields() {
    this.clientData = {};
  }

  setClientId(id: number) {
    this.clientData = {};
    this.clientId = id;
    this.clientService.getClientById(id)
      .subscribe(
        (clientData: any) => {
          this.clientData = clientData;
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
  }

  createClient() {
    this.clientService.createClient(this.clientData)
      .subscribe(
        () => {
          this.clientService.getClients()
            .subscribe(
              (clients: ClientListData[]) => {
                this.clients = clients;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.CREATED.CLIENT'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.CREATED.CLIENT'));
        }
      );
      this.clientData = {};
  }

  updateClient() {
    this.clientService.updateClient(this.clientId, this.clientData)
      .subscribe(
        () => {
          this.clientService.getClients()
            .subscribe(
              (clients: ClientListData[]) => {
                this.clients = clients;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.UPDATED.CLIENT'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.UPDATED.CLIENT'));
        }
      );
      this.clientData = {};
  }

  deleteClient() {
    this.clientService.deleteClient(this.clientId)
      .subscribe(
        () => {
          this.clientService.getClients()
            .subscribe(
              (clients: ClientListData[]) => {
                this.clients = clients;
                this.toastr.success(this.translate.instant('ALERT.SUCCESS.DELETED.CLIENT'));
              },
              (error: any) => {
                this.error = error;
                this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
              }
            );
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(this.translate.instant('ALERT.ERROR.DELETED.CLIENT'));
        }
      );
  }

  search(): ClientListData[] {
    if (!this.searchText) {
      return this.clients;
    }
    return this.clients.filter(client =>
      client.fullName.toLowerCase().includes(this.searchText.toLowerCase())
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
