import { Action } from '@ngrx/store';
import { CartItem } from '../models/cart-item';

export const LOAD_REQUEST = '[Equipment] Load Request';
export const LOAD_SUCCESS = '[Equipment] Load Success';
export const LOAD_FAILURE = '[Equipment] Load Failure';

export const ADD_TO_CART = '[Equipment] Add to cart';

export class LoadRequest implements Action {
  readonly type = LOAD_REQUEST;
}

export class LoadSuccess implements Action {
  readonly type = LOAD_SUCCESS;

  constructor(
    public payload: { machineNames: string[] }
  ) { }
}

export class LoadFailure implements Action {
  readonly type = LOAD_FAILURE;
}

export class AddToCart implements Action {
  readonly type = ADD_TO_CART;

  constructor(
    public payload: { cartItem: CartItem }
  ) { }
}

export type Actions = LoadRequest | LoadSuccess | LoadFailure
  | AddToCart;

