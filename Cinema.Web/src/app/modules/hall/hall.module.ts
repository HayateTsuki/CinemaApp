import { NgModule } from '@angular/core';
import { HallRoutingModule } from './hall-routing.module';
import { HallsComponent } from './components/halls/halls.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { HallComponent } from './components/hall/hall.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    HallsComponent,
    HallComponent
  ],
  imports: [
    SharedModule,
    HallRoutingModule,
    NgxDatatableModule,
  ],
  providers: [],
  bootstrap: [HallComponent]
})
export class HallModule { }
