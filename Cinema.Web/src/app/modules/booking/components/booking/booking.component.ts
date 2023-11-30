import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Booking } from 'src/app/shared/models/booking.model';
import { BookingService } from '../../services/booking.service';

@Component({
  selector: 'cinema-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']
})
export class BookingComponent implements OnInit, OnDestroy {

  constructor(private bookingService: BookingService, private route: ActivatedRoute) { }
  booking: Booking;
  private routeSub: Subscription;

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.bookingService.getById$(params['id']).subscribe(x=> {
        this.booking = x.data;
      } );
    });
  }

  ngOnDestroy(): void {
    this.routeSub.unsubscribe();
  }
}
