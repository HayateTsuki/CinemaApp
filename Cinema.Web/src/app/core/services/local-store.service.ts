import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import jwtDecode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class LocalStoreService {
  private static ACCESS_TOKEN = 'access_token';
  private static USER = 'id';

  public loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  addToken(token: string) {
    const myToken = this.getToken();
    if (myToken != null) {
        this.removeToken();
    }
    localStorage.setItem(LocalStoreService.ACCESS_TOKEN, token);
    const decoded = jwtDecode(token) as any;
    localStorage.setItem(LocalStoreService.USER, decoded[LocalStoreService.USER]);
    this.loggedIn.next(true);
  }

  getToken(): string | null {
      return localStorage.getItem(LocalStoreService.ACCESS_TOKEN);
  }

  getUser(): string | null {
    return localStorage.getItem(LocalStoreService.USER);
  }

  removeToken(): void {
    localStorage.removeItem(LocalStoreService.ACCESS_TOKEN);
    localStorage.removeItem(LocalStoreService.USER);
    this.loggedIn.next(false);
  }
}

