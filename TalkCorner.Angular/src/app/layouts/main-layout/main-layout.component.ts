import { Component } from '@angular/core';
import { Router} from "@angular/router";

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  constructor(private router: Router) {
  }
  currentYear = new Date().getFullYear();
  sidebarOpen = false;

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

  closeSidebar() {
    this.sidebarOpen = false;
  }

  navigateToLogin() {
    this.router.navigate(['login']);
  }

  navigateToBoards() {
    this.router.navigate(['boards']);
  }
}
