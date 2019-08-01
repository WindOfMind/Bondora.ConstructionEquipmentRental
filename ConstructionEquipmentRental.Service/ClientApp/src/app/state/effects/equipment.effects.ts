import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { EquipmentService } from 'src/app/data-services/equipment.service';
import * as equipmentActions from '../equipment.actions';
import { catchError, map, mergeMap} from 'rxjs/operators';

@Injectable()
export class EquipmentEffects {
  @Effect() equipmentLoad$: Observable<Action> = this.actions$
    .pipe(
      ofType(equipmentActions.LOAD_REQUEST),
      mergeMap(() =>
        this.equipmentService.fetchMachineNames()
          .pipe(
             map((machineNames: string[]) => new equipmentActions.LoadSuccess({machineNames})),
             catchError(error => of(new equipmentActions.LoadFailure()))
          )
      )
    );

  constructor(
    private actions$: Actions,
    private equipmentService: EquipmentService
  ) { }
}
