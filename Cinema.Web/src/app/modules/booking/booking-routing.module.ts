import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookingComponent } from './components/booking/booking.component';
import { BookingsComponent } from './components/bookings/bookings.component';

const routes: Routes = [
  {path: '', component: BookingsComponent},
  {path: ':id', component: BookingComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookingRoutingModule { }
