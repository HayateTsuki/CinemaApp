import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';

import { BookingRoutingModule } from './booking-routing.module';
import { BookingComponent } from './components/booking/booking.component';
import { BookingsComponent } from './components/bookings/bookings.component';


@NgModule({
  declarations: [
    BookingComponent,
    BookingsComponent
  ],
  imports: [
    SharedModule,
    BookingRoutingModule
  ]
})
export class BookingModule { }
