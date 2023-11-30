import { NgModule } from '@angular/core';

import { ScreeningRoutingModule } from './screening-routing.module';
import { ScreeningComponent } from './components/screening/screening.component';
import { ScreeningsComponent } from './components/screenings/screenings.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { BookingFormComponent } from './components/screening/booking-form/booking-form.component';


@NgModule({
  declarations: [
    ScreeningComponent,
    ScreeningsComponent,
    BookingFormComponent,
  ],
  imports: [
    SharedModule,
    ScreeningRoutingModule
  ]
})
export class ScreeningModule { }
