import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from "rxjs";
import {jwtDecode} from "jwt-decode";

const TOKEN_KEY = 'talkcorner_token';
const ROLE_CLAIM = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

export interface JwtPayload {
  exp: number;
  [key: string]: any;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authStatusSubject = new BehaviorSubject<boolean>(this.hasValidToken());
  authStatus$: Observable<boolean> = this.authStatusSubject.asObservable();
  private isAdminSubject = new BehaviorSubject<boolean>(false);
  isAdmin$: Observable<boolean> = this.isAdminSubject.asObservable();

  constructor() {
    this.authStatusSubject.next(this.hasValidToken());
    this.isAdminSubject.next(this.isAdmin());
  }

  get token(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  getRolesFromToken(): string[] | null {
    const token = this.token;
    if (!token) return null;
    try {
      const payload = jwtDecode<JwtPayload>(token);
      const role = payload[ROLE_CLAIM];
      if (!role) return null;
      if (Array.isArray(role)) return role;
      return [role]; // Single role as array
    } catch {
      return null;
    }
  }

  isAdmin(): boolean {
    const roles = this.getRolesFromToken();
    return !!roles && roles.includes('Administrator');
  }

  hasToken(): boolean {
    return !!this.token;
  }

  hasValidToken(): boolean {
    const token = this.token;

    if (!token) {
      return false;
    }

    try {
      const payload = jwtDecode<JwtPayload>(token);
      return !!payload.exp && Date.now() < payload.exp * 1000;
    } catch {
      return false;
    }
  }

  setToken(token: string) {
    localStorage.setItem(TOKEN_KEY, token);
    this.authStatusSubject.next(this.hasValidToken());
    this.isAdminSubject.next(this.isAdmin());
  }

  clearToken() {
    localStorage.removeItem(TOKEN_KEY);
    this.authStatusSubject.next(false);
    this.isAdminSubject.next(false);
  }

  login(token: string) {
    this.setToken(token);
  }

  logout() {
    this.clearToken();
  }
}
