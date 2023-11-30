import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Screening } from 'src/app/shared/models/screening.model';
import { ScreeningService } from '../../services/screening.service';

@Component({
  selector: 'cinema-screening',
  templateUrl: './screening.component.html',
  styleUrls: ['./screening.component.scss']
})
export class ScreeningComponent implements OnInit {

  constructor(private screeningService: ScreeningService, private route: ActivatedRoute) {}

  screening: Screening;
  showForm = false;
  private routeSub: Subscription;
  dates: Date[] = [];
  movieId: number;
  rows: number;
  seats: number;

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.screeningService.getById$(params['id']).subscribe((x) => {
        this.screening = x.data;
        this.movieId = x.data.movie.id;
        this.screeningService.get$(this.movieId).subscribe((x) => {
          x.list.forEach(e => {
            this.dates.push(e.date);
          });
        });
      });
    });
  }

  ngOnDestroy(): void {
    this.routeSub.unsubscribe();
  }
}
