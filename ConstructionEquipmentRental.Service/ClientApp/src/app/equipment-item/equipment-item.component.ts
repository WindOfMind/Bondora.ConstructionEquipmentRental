import { Component, Input, ElementRef, ViewChild } from '@angular/core';
import * as equipmentActions from '../state/equipment.actions';
import { Store } from '@ngrx/store';
import { State } from '../state/equipment.reducer';
import { CartItem } from '../models/cart-item';

@Component({
  selector: 'app-equipment-item',
  templateUrl: './equipment-item.component.html',
  styleUrls: ['./equipment-item.component.scss']
})
export class EquipmentItemComponent {
  @Input() name: string;

  @ViewChild('rentalDaysCountInput', {static: false}) rentalDaysCountInput: ElementRef;

  constructor(private store: Store<State>) {}

  onClickAddToCart(daysCount: number): void {
    if (!daysCount || daysCount <= 0) {
      return;
    }

    const cartItem: CartItem = {
      name: this.name,
      rentalDays: daysCount
    };

    this.store.dispatch(new equipmentActions.AddToCart({cartItem}));

    this.rentalDaysCountInput.nativeElement.value = '';
  }
}
