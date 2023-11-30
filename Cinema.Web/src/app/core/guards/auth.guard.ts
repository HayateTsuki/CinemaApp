import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtService } from '../services/jwt.service';
import { LocalStoreService } from '../services/local-store.service';


@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private localStoreService: LocalStoreService, private router: Router, private jwtService: JwtService) { }
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    let token = this.localStoreService.getToken();

    if (token != null) {
      if (token.length < 3 || this.jwtService.isTokenExpired(token)) {
        this.localStoreService.removeToken();
      } else {
        this.localStoreService.loggedIn.next(true);
        return true;
      }
    }
    this.router.navigate(['/identity']);
    return false;
  }
}
