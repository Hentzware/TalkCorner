import {Component} from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  currentYear = new Date().getFullYear();

  constructor(private router: Router, public authService: AuthService) {}

  navigateToLogin() {
    this.router.navigate(['login']);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['boards']);
  }
}
