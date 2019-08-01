import { createFeatureSelector, createSelector } from '@ngrx/store';
import { State } from './equipment.reducer';

export const getEquipmentState = createFeatureSelector<State>('equipment');

export const getIsFetchingEquipment = createSelector(
  getEquipmentState,
  ({ machineNamesFetch }: State) => machineNamesFetch.loading
);

export const getIsFetchingEquipmentFailed = createSelector(
  getEquipmentState,
  ({ machineNamesFetch }: State) => machineNamesFetch.error !== null
);

export const getMachineNames = createSelector(
  getEquipmentState,
  ({ machineNamesFetch }) => machineNamesFetch.entity
);

export const getCartItems = createSelector(
  getEquipmentState,
  ({ cartItems }) => cartItems
);
