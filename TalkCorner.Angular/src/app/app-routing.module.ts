import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./pages/login/login.component";
import {RegisterComponent} from "./pages/register/register.component";
import {HomeComponent} from "./pages/home/home.component";
import {BoardsComponent} from "./pages/boards/boards.component";
import {MainLayoutComponent} from "./layouts/main-layout/main-layout.component";
import {AuthLayoutComponent} from "./layouts/auth-layout/auth-layout.component";

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent, // Enthält Header, Sidebar, Footer
    children: [
      { path: '', component: HomeComponent },
      { path: 'boards', component: BoardsComponent },
      // ...
    ]
  },
  {
    path: '',
    component: AuthLayoutComponent, // Enthält nur <router-outlet>
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
