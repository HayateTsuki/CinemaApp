import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResult } from 'src/app/shared/models/list-result.model';
import { Screening } from 'src/app/shared/models/screening.model';
import { SingleResult } from 'src/app/shared/models/single-result.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ScreeningService {

constructor(private http: HttpClient) { }

  getById$(id: number): Observable<SingleResult<Screening>> {
    return this.http.get<SingleResult<Screening>>(`${environment.apiUrl}/screenings/${id}`);
  }

  get$(movieId?: number): Observable<ListResult<Screening>> {
    let params = new HttpParams();
    if (movieId) {
      params = params.set('movieId', movieId);
    }

    return this.http.get<ListResult<Screening>>(`${environment.apiUrl}/screenings`, { params });
  }
}