import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RequiredComponent } from './components/required/required.component';
import { HomeComponent } from './components/home/home.component';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { TokenInterceptor } from './helpers/token.interceptor';
import { ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { SettingsComponent } from './components/settings/settings.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { ClientListComponent } from './components/client/client-list/client-list.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeListComponent } from './components/employee/employee-list/employee-list.component';
import { CarListComponent } from './components/car/car-list/car-list.component';
import { CarDetailsComponent } from './components/car/car-details/car-details.component';
import { ClientDetailsComponent } from './components/client/client-details/client-details.component';
import { RentalListComponent } from './components/rental/rental-list/rental-list.component';
import { RentalDetailsComponent } from './components/rental/rental-details/rental-details.component';
import { PaymentListComponent } from './components/payment/payment-list/payment-list.component';
import { ToastrModule } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RequiredComponent,
    HomeComponent,
    NavbarComponent,
    FooterComponent,
    SettingsComponent,
    ClientListComponent,
    EmployeeListComponent,
    CarListComponent,
    CarDetailsComponent,
    ClientDetailsComponent,
    RentalListComponent,
    RentalDetailsComponent,
    PaymentListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgxChartsModule,
    ToastrModule.forRoot(),
    FormsModule,
    NgbModule,
    TranslateModule.forRoot({
      loader:{
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    provideClientHydration(),
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
