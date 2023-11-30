import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Hall } from 'src/app/shared/models/hall.model';
import { HallService } from '../../services/hall.service';
import { ColumnMode } from '@swimlane/ngx-datatable';

@Component({
  selector: 'cinema-hall',
  templateUrl: './hall.component.html',
  styleUrls: ['./hall.component.scss'],
})
export class HallComponent implements OnInit {
  hall: Hall;
  private routeSub: Subscription;
  subscription: Subscription;
  submitted = false;
  isNew = false;
  reorderable = true;
  ColumnMode = ColumnMode;

  constructor(
    private hallService: HallService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.routeSub = this.route.params.subscribe((params) => {
      const id = params['id'];
      if (id <= 0) {
        this.isNew = true;
        this.hall = new Hall();
      } else {
        this.hallService.getById$(params['id']).subscribe((x) => {
          this.hall = x.data;
        });
      }
    });
  }

  ngOnDestroy(): void {
    this.routeSub.unsubscribe();
  }

  onSubmit(f: NgForm) {
    this.submitted = true;
    if (f.valid) {
      if (this.isNew) {
        this.hallService
          .post$(this.hall)
          .subscribe(() => this.router.navigate(['/halls/']));
      } else {
        this.hallService
          .put$(this.hall)
          .subscribe(() => this.router.navigate(['/halls/']));
      }
    }
  }

  backToHalls() {
    this.router.navigate(['/halls/']);
  }
}
