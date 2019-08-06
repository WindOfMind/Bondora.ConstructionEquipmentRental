import { APP_BASE_HREF } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { first } from 'rxjs/operators';

const ICONS = [
  { name: 'spinner', url: 'assets/svg/spinner.svg' }
];

@Injectable()
export class AppIconsService {

  constructor(
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    @Inject(APP_BASE_HREF) private baseHref: string
  ) { }

  register() {
    this.iconRegistry.registerFontClassAlias('app');

    ICONS.forEach(icon => {
      this.iconRegistry.addSvgIcon(
        icon.name,
        this.sanitizer.bypassSecurityTrustResourceUrl(this.baseHref + icon.url));

      this.iconRegistry.getNamedSvgIcon(icon.name).pipe(first()).subscribe(() => null);
    });
  }
}
