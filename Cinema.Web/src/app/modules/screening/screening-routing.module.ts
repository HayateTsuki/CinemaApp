import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ScreeningComponent } from './components/screening/screening.component';
import { ScreeningsComponent } from './components/screenings/screenings.component';

const routes: Routes = [
  { path: '', component: ScreeningsComponent },
  { path: ':id', component: ScreeningComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ScreeningRoutingModule { }
