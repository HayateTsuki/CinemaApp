import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginUserViewModel } from 'src/app/shared/models/login-user-view.model';
import { LoginUserResult } from 'src/app/shared/models/login-user-result.model';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) { }

  loginUser$(loginUserViewModel: LoginUserViewModel): Observable<LoginUserResult> {
    return this.http.post<LoginUserResult>(`${environment.apiUrl}/account/login`, JSON.stringify(loginUserViewModel));
  }
}
