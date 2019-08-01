import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { EquipmentEffects } from './equipment.effects';

@NgModule({
  imports: [
    EffectsModule.forFeature([
      EquipmentEffects
    ])
  ]
})
export class EquipmentEffectsModule { }
