import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Booking } from 'src/app/shared/models/booking.model';
import { Screening } from 'src/app/shared/models/screening.model';
import { BookingService } from '../../../../booking/services/booking.service';

@Component({
  selector: 'cinema-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.scss'],
})
export class BookingFormComponent implements OnInit {
  @Output() formWasCancelled = new EventEmitter<void>();
  @Input() screening: Screening;
  @Input() screeningDates: Date[];
  newBooking: Booking;
  choosenDate: Date;
  form: FormGroup;
  dropdownContent: string = "Wybierz z listy";

  constructor(
    private bookingService: BookingService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      row: [null, [Validators.required, Validators.min(1), Validators.max(this.screening.hall.rows)]],
      seat: [null, [Validators.required, Validators.min(1), Validators.max(this.screening.hall.seatsPerRow)]],
      date: [null, [Validators.required]],
    });
    this.newBooking = new Booking;
    this.newBooking.isConfirmed = false;
    this.newBooking.screening = this.screening;
  }
  
  get f() {
    return this.form.controls;
  }

  onFormSubmit() {
    this.newBooking.date = new Date();
    this.bookingService.post$(this.newBooking).subscribe(() => this.router.navigate(['/bookings/']));
  }

  onFormCancel() {
    this.formWasCancelled.emit();
  }

  onUpdateRow(event: Event) {
    this.newBooking.row = parseInt((<HTMLInputElement>event.target).value);
  }

  onUpdateSeat(event: Event) {
    this.newBooking.seat = parseInt((<HTMLInputElement>event.target).value);
  }

  onUpdateDate(event: Event, date: Date) {
    this.dropdownContent = (<HTMLBodyElement>event.currentTarget).innerHTML;
    this.choosenDate = date;
  }
}
