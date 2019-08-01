import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments';

export const BASE_URL = `${environment.equipmentApiEndpoint}`;

@Injectable()
export class EquipmentService {
  constructor(private http: HttpClient) { }

  fetchMachineNames(): Observable<string[]> {
    return this.http.get<string[]>(BASE_URL);
  }
}
