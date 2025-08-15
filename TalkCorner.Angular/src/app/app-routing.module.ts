import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./pages/login/login.component";
import {RegisterComponent} from "./pages/register/register.component";
import {BoardsComponent} from "./pages/boards/boards.component";
import {MainLayoutComponent} from "./layouts/main-layout/main-layout.component";
import {AuthLayoutComponent} from "./layouts/auth-layout/auth-layout.component";
import {AdminBoardsComponent} from "./pages/admin/admin-boards/admin-boards.component";
import {AdminDashboardComponent} from "./pages/admin/admin-dashboard/admin-dashboard.component";
import {AdminUsersComponent} from "./pages/admin/admin-users/admin-users.component";
import {AdminSettingsComponent} from "./pages/admin/admin-settings/admin-settings.component";
import {BoardDetailComponent} from "./pages/board-detail/board-detail.component";

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: 'boards', component: BoardsComponent },
      { path: 'boards/:id', component: BoardDetailComponent },
      { path: 'admin/dashboard', component: AdminDashboardComponent },
      { path: 'admin/boards', component: AdminBoardsComponent },
      { path: 'admin/users', component: AdminUsersComponent },
      { path: 'admin/settings', component: AdminSettingsComponent }
    ]
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
