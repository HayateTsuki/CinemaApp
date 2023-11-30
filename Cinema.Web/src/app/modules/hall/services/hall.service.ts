import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hall } from 'src/app/shared/models/hall.model';
import { ListResult } from 'src/app/shared/models/list-result.model';
import { SingleResult } from 'src/app/shared/models/single-result.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HallService {


constructor(private http: HttpClient) { }

  getById$(id: number): Observable<SingleResult<Hall>> {
    return this.http.get<SingleResult<Hall>>(`${environment.apiUrl}/halls/${id}`);
  }

  get$(): Observable<ListResult<Hall>> {
    return this.http.get<ListResult<Hall>>(`${environment.apiUrl}/halls`);
  }

  post$(newHall: Hall): Observable<void>{
    return this.http.post<void>(`${environment.apiUrl}/halls`, JSON.stringify(newHall));
  }

  put$(updatedHall: Hall){
    return this.http.put<void>(`${environment.apiUrl}/halls`, JSON.stringify(updatedHall));
  }
}
