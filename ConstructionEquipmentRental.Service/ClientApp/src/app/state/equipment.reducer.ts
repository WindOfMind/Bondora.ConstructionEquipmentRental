import { Fetch } from '../models/fetch';
import * as equipmentActions from './equipment.actions';
import { FetchError } from '../models/fetch-error';
import { CartItem } from '../models/cart-item';

export interface State {
  machineNamesFetch: Fetch<string[]>;
  cartItems: CartItem[];
}

export const initialState: State = {
  machineNamesFetch: {
    id: null,
    entity: [],
    error: null,
    loading: false
  },
  cartItems: []
};

export function equipmentReducer(state = initialState, action: equipmentActions.Actions): State {
  switch (action.type) {
    case equipmentActions.LOAD_REQUEST: {
      return {
        ...state,
        machineNamesFetch: {
          ...state.machineNamesFetch,
          loading: true,
          error: null
        }
      };
    }

    case equipmentActions.LOAD_SUCCESS: {
      const { machineNames } = action.payload;

      return {
        ...state,
        machineNamesFetch: {
          ...state.machineNamesFetch,
          entity: machineNames,
          loading: false,
          error: null
        }
      };
    }

    case equipmentActions.LOAD_FAILURE: {
      return {
        ...state,
        machineNamesFetch: {
          ...state.machineNamesFetch,
          entity: [],
          loading: false,
          error: FetchError.Error
        }
      };
    }

    case equipmentActions.ADD_TO_CART: {
      const { cartItem } = action.payload;

      return {
        ...state,
        cartItems: [...state.cartItems, cartItem]
      };
    }
  }
}
