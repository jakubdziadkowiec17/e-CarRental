import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { LoginData } from '../../interfaces/login-data';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;

  constructor(private accountService: AccountService, private translate: TranslateService, private toastr: ToastrService, private router: Router) {}

  ngOnInit() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
    });
  }

  public onSubmit() {
    if (this.loginForm.valid) {
      const loginData: LoginData = {
        email: this.loginForm.value.email,
        password: this.loginForm.value.password
      };
      this.accountService.login(loginData).subscribe(
        () => {
          this.router.navigate(['/']);
        },
        error => {
          this.toastr.error(this.translate.instant('ALERT.ERROR.LOG.IN'));
        }
      );
    } else {
      this.toastr.error(this.translate.instant('ALERT.ERROR.LOG.IN'));
    }
    this.loginForm.reset();
  }
}