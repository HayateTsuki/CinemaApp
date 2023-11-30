import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { LocalStoreService } from 'src/app/core/services/local-store.service';
import { LoginUserResult } from 'src/app/shared/models/login-user-result.model';
import { LoginUserViewModel } from 'src/app/shared/models/login-user-view.model';

@Component({
  selector: 'cinema-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  @ViewChild('password') passwordInput: ElementRef<HTMLInputElement>;
  
  form: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private localStore: LocalStoreService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(6)]],
    });
  }

  get f() {
    return this.form.controls;
  }

  onFormSubmit(f: any) {
    if (f.valid) {
      const model = new LoginUserViewModel(
        f.controls.email.value,
        f.controls.password.value
      );
      this.authService
        .loginUser$(model)
        .subscribe((result: LoginUserResult) => {
          this.localStore.addToken(result.token);
          if (result.token) {
            this.router.navigateByUrl('screenings');
          }
        });
    }
    f.controls.password.reset();
    this.passwordInput.nativeElement.focus();
  }
}
