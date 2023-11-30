import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Booking } from 'src/app/shared/models/booking.model';
import { ListResult } from 'src/app/shared/models/list-result.model';
import { SingleResult } from 'src/app/shared/models/single-result.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

constructor(private http: HttpClient) { }

  getById$(id: number): Observable<SingleResult<Booking>> {
    return this.http.get<SingleResult<Booking>>(`${environment.apiUrl}/bookings/${id}`);
  }

  get$(): Observable<ListResult<Booking>> {
    return this.http.get<ListResult<Booking>>(`${environment.apiUrl}/bookings`);
  }

  post$(newBooking: Booking): Observable<void>{
    return this.http.post<void>(`${environment.apiUrl}/bookings`, JSON.stringify(newBooking));
  }
}
