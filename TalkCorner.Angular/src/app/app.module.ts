import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { ReactiveFormsModule } from "@angular/forms";
import { RegisterComponent } from './pages/register/register.component';
import { BoardsComponent } from './pages/boards/boards.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { authInterceptor } from "./interceptors/auth.interceptor";
import { API_BASE_URL } from "./api/board-client";
import { environment } from "../environments/environment";
import { AdminBoardsComponent } from './pages/admin/admin-boards/admin-boards.component';
import { AdminDashboardComponent } from './pages/admin/admin-dashboard/admin-dashboard.component';
import { AdminUsersComponent } from './pages/admin/admin-users/admin-users.component';
import { AdminSettingsComponent } from './pages/admin/admin-settings/admin-settings.component';
import { HasAnyRoleDirective } from "./core/directives/has-any-role.directive";
import { HasAllRoleDirective } from "./core/directives/has-all-role.directive";
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { AdminBoardDialogComponent } from './dialogs/admin-board-dialog/admin-board-dialog.component';
import { DeleteConfirmDialogComponent } from './dialogs/delete-confirm-dialog/delete-confirm-dialog.component';
import { BoardDetailComponent } from './pages/board-detail/board-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    BoardsComponent,
    MainLayoutComponent,
    AuthLayoutComponent,
    AdminBoardsComponent,
    AdminDashboardComponent,
    AdminUsersComponent,
    AdminSettingsComponent,
    HasAnyRoleDirective,
    HasAllRoleDirective,
    AdminBoardDialogComponent,
    DeleteConfirmDialogComponent,
    BoardDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    provideHttpClient(
      withInterceptors([
        authInterceptor,
      ])
    ),
    { provide: API_BASE_URL, useValue: environment.apiBaseUrl },
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
