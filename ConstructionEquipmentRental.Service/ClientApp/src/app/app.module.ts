import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { APP_BASE_HREF, PlatformLocation } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { AppComponent } from './app.component';
import { getBaseHref } from './core/base-href.factory';
import { AppIconsService } from './core/services/app-icons.service';
import { EquipmentService } from './data-services/equipment.service';
import { InvoiceService } from './data-services/invoice.service';
import { SignalRService } from './data-services/signalr.service';
import { EquipmentCartComponent } from './equipment-cart/equipment-cart.component';
import { EquipmentItemComponent } from './equipment-item/equipment-item.component';
import { EquipmentListComponent } from './equipment-list/equipment-list.component';
import { EquipmentEffects } from './state/effects/equipment.effects';
import { equipmentReducer } from './state/equipment.reducer';

@NgModule({
  declarations: [
    AppComponent,
    EquipmentItemComponent,
    EquipmentListComponent,
    EquipmentCartComponent
  ],
  imports: [
    MatIconModule,
    HttpClientModule,
    StoreModule.forRoot({equipment: equipmentReducer}),
    EffectsModule.forRoot([EquipmentEffects]),
    BrowserModule,
    FormsModule
  ],
  providers: [
    EquipmentService,
    InvoiceService,
    SignalRService,
    AppIconsService,
    {
      provide: APP_BASE_HREF,
      useFactory: getBaseHref,
      deps: [PlatformLocation]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(appIconsService: AppIconsService) {
    appIconsService.register();
  }
}
