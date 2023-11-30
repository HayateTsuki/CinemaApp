import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './core/components/main/main.component';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      { path: '', redirectTo: 'screenings', pathMatch: 'full' },
      {
        path: 'screenings',
        loadChildren: () =>
          import('./modules/screening/screening.module').then(
            (m) => m.ScreeningModule
          ),
      },
      {
        path: 'bookings',
        loadChildren: () =>
          import('./modules/booking/booking.module').then(
            (m) => m.BookingModule
          ),
      },
      {
        path: 'halls',
        loadChildren: () =>
          import('./modules/hall/hall.module').then((m) => m.HallModule),
      },
    ],
    canActivate: [AuthGuard],
  },
  {
    path: 'identity',
    loadChildren: () =>
      import('./modules/identity/identity.module').then(
        (m) => m.IdentityModule
      ),
  },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', redirectTo: '/not-found', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
