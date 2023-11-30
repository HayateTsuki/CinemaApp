import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { Booking } from 'src/app/shared/models/booking.model';
import { BookingService } from '../../services/booking.service';

@Component({
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.scss'],
})
export class BookingsComponent implements OnInit {
  constructor(private bookingService: BookingService, private router: Router) {}

  bookings: Booking[] = [];
  ColumnMode = ColumnMode;

  ngOnInit() {
    this.bookingService.get$().subscribe(result => this.bookings = result.list);
  }

  viewBookingDetails(id: number) {
    this.router.navigate(['/bookings/'+ id])
  }
}
