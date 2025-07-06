import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'e-CarRental';
  constructor(
    private router: Router,
    public translate: TranslateService,
    private cookieService: CookieService
  ) {
    const languageKey = 'LANGUAGE';
    const languages = ['pl', 'en'];
    translate.addLangs(languages);

    const cookieLanguage = this.cookieService.get(languageKey);
    const currentLanguage = cookieLanguage && languages.includes(cookieLanguage) ? cookieLanguage : languages[0];

    translate.setDefaultLang(currentLanguage);
    translate.use(currentLanguage);
    this.cookieService.set(languageKey, currentLanguage);
  }

  switchLanguage(lang: string){
    this.translate.use(lang);
  }

  isLoginPanel(): boolean {
    const currentPath = this.router.url;
    
    if (currentPath.includes('/login')) {
      return true;
    }
    return false;
  }
}
