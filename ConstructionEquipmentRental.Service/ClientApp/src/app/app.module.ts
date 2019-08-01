import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { EquipmentItemComponent } from './equipment-item/equipment-item.component';
import { EquipmentListComponent } from './equipment-list/equipment-list.component';
import { EquipmentCartComponent } from './equipment-cart/equipment-cart.component';
import { StoreModule } from '@ngrx/store';
import { equipmentReducer } from './state/equipment.reducer';
import { EffectsModule } from '@ngrx/effects';
import { EquipmentEffects } from './state/effects/equipment.effects';
import { EquipmentService } from './data-services/equipment.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { InvoiceService } from './data-services/invoice.service';
import { SignalRService } from './data-services/signalr.service';

@NgModule({
  declarations: [
    AppComponent,
    EquipmentItemComponent,
    EquipmentListComponent,
    EquipmentCartComponent
  ],
  imports: [
    HttpClientModule,
    StoreModule.forRoot({equipment: equipmentReducer}),
    EffectsModule.forRoot([EquipmentEffects]),
    BrowserModule,
    FormsModule
  ],
  providers: [
    EquipmentService,
    InvoiceService,
    SignalRService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
