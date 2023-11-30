import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { Screening } from 'src/app/shared/models/screening.model';
import { ScreeningService } from '../../services/screening.service';

@Component({
  templateUrl: './screenings.component.html',
  styleUrls: ['./screenings.component.scss']
})
export class ScreeningsComponent implements OnInit {
  constructor(private screeningService: ScreeningService, private router: Router) {}

  screenings: Screening[] = [];
  ColumnMode = ColumnMode;

  ngOnInit() {
    this.screeningService.get$().subscribe(result => this.screenings = result.list);
  }

  viewScreeningDetails(id: number) {
    this.router.navigate(['/screenings/'+ id])
  }
}
