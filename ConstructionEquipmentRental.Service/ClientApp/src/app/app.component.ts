import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { State } from './state/equipment.reducer';
import * as equipmentActions from './state/equipment.actions';
import { getMachineNames, getCartItems } from './state/equipment.selector';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  names$ = this.store.select(getMachineNames);
  cartItems$ = this.store.select(getCartItems);

  constructor(private store: Store<State>) {}

  ngOnInit(): void {
    this.store.dispatch(new equipmentActions.LoadRequest());
  }
}
