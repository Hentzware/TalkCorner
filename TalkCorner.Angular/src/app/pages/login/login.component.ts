import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {ApiClient, LoginCommand} from "../../api/board-client";
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  form: FormGroup;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private api: ApiClient,
    private router: Router,
    private authService: AuthService) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  setDemoUserCredentials(): void {
    this.form.setValue({
      email: 'user@localhost.de',
      password: 'Passw0rd'
    });
    this.error = null;
  }

  setDemoAdminCredentials(): void {
    this.form.setValue({
      email: 'admin@localhost.de',
      password: 'Passw0rd'
    });
    this.error = null;
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    let loginCommand = new LoginCommand();
    loginCommand.email = this.form.controls['email'].value;
    loginCommand.password = this.form.controls['password'].value;

    this.api.login(loginCommand).subscribe({
      next: (response) => {
        if (response.token != null) {
          this.authService.login(response.token);
          this.router.navigate(['/boards']);
        } else {
          this.error = "Kein Token erhalten.";
        }
      },
      error: (err) => {
        this.error = typeof err === 'string' ? err : (err?.title ?? "Login fehlgeschlagen.");
      }
    });
  }
}
