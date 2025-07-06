import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserData } from '../../interfaces/user-data';
import { AccountService } from '../../services/account.service';
import { ResetPasswordData } from '../../interfaces/reset-password-data';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent {
  currentUser!: UserData;
  editProfileForm!: FormGroup;
  resetPasswordForm: FormGroup;
  currentLanguage!: string;
  private languageKey = 'LANGUAGE';

  constructor(private accountService: AccountService, private cookieService: CookieService, private toastr: ToastrService, private formBuilder: FormBuilder, private router: Router, private translate: TranslateService) {
    this.editProfileForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      secondName: [''],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      pesel: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      address: ['', Validators.required],
      areaCode: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    });

    if (typeof window !== 'undefined' && window.localStorage) {
      this.accountService.getCurrentUserSettings().subscribe(user => {
        this.currentUser = user;
        this.editProfileForm.patchValue({
          id: this.currentUser.id,
          firstName: this.currentUser.firstName,
          secondName: this.currentUser.secondName,
          lastName: this.currentUser.lastName,
          email: this.currentUser.email,
          pesel: this.currentUser.pesel,
          dateOfBirth: this.currentUser.dateOfBirth,
          address: this.currentUser.address,
          areaCode: this.currentUser.areaCode,
          phoneNumber: this.currentUser.phoneNumber
        });
      },
      error => {
        this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
      }
    );
  
      this.currentLanguage = this.cookieService.get(this.languageKey) || 'pl';
      this.translate.setDefaultLang(this.currentLanguage);
      this.translate.use(this.currentLanguage);
    }
  
    this.resetPasswordForm = this.formBuilder.group({
      oldPassword: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
  }
  
  public editProfile() {
    if (this.editProfileForm.valid) {
      const userData: UserData = {
        id: this.currentUser.id,
        firstName: this.editProfileForm.value.firstName,
        secondName: this.editProfileForm.value.secondName,
        lastName: this.editProfileForm.value.lastName,
        email: this.editProfileForm.value.email,
        pesel: this.editProfileForm.value.pesel,
        dateOfBirth: this.editProfileForm.value.dateOfBirth,
        address: this.editProfileForm.value.address,
        areaCode: this.editProfileForm.value.areaCode,
        phoneNumber: this.editProfileForm.value.phoneNumber
      };

      this.accountService.editProfile(userData).subscribe(
        () => {
          this.router.navigate(['/']);
          this.toastr.success(this.translate.instant('ALERT.SUCCESS.SETTINGS.UPDATED'));
        },
        error => {
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
    }
  }

  public resetPassword() {
    if (this.resetPasswordForm.valid) {
      const resetPasswordData: ResetPasswordData = {
        oldPassword: this.resetPasswordForm.value.oldPassword,
        password: this.resetPasswordForm.value.password,
        confirmPassword: this.resetPasswordForm.value.confirmPassword
      };
      this.accountService.resetPassword(resetPasswordData).subscribe(
        () => {
          this.router.navigate(['/']);
          this.toastr.success(this.translate.instant('ALERT.SUCCESS.SETTINGS.RESETPASSWORD'));
        },
        error => {
          this.toastr.error(this.translate.instant('ALERT.ERROR.DOWNLOADING.DATA'));
        }
      );
    }
  }

  selectLanguage(event: any) {
    const language = event.target.value;
    if (language) {
      this.translate.setDefaultLang(language);
      this.translate.use(language);
      this.cookieService.set(this.languageKey, language);
    } else {
      console.error('Error: selected value is null');
    }
  }
}