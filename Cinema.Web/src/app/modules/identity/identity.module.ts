import { NgModule } from '@angular/core';
import { IdentityRoutingModule } from './identity-routing.module';
import { LoginComponent } from './components/login/login.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    SharedModule,
    IdentityRoutingModule
  ]
})
export class IdentityModule { }
