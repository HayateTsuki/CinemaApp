import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HallsComponent } from './components/halls/halls.component';
import { HallComponent } from './components/hall/hall.component';

const routes: Routes = [
  { path: '', component: HallsComponent },
  { path: ':id', component: HallComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HallRoutingModule { }
