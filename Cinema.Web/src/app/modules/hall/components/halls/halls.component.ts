import { Component, OnInit } from '@angular/core';
import { Hall } from 'src/app/shared/models/hall.model';
import { Router } from '@angular/router';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { HallService } from '../../services/hall.service';

@Component({
  selector: 'cinema-hall',
  templateUrl: './halls.component.html',
  styleUrls: ['./halls.component.scss']
})
export class HallsComponent implements OnInit {
  constructor(private hallService: HallService, private router: Router) { }

  halls: Hall[] = [];
  reorderable = true;
  ColumnMode = ColumnMode;

  ngOnInit() {
    this.hallService.get$().subscribe(result => this.halls = result.list);
  }

  viewHallDetails(id: number) {
    this.router.navigate(['/halls/', id])
  }

}
