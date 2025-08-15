import { Component, OnInit } from '@angular/core';
import { ApiClient, GetAllUsersDto, GetUserByIdDto, UpdateUserCommand } from '../../../api/board-client';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit {
  users: GetAllUsersDto[] = [];
  userForm: FormGroup;
  editingUser: GetUserByIdDto | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private api: ApiClient,
    private fb: FormBuilder
  ) {
    this.userForm = this.fb.group({
      displayName: ['', [Validators.required, Validators.maxLength(100)]]
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading = true;
    this.api.usersAll().subscribe({
      next: users => {
        this.users = users;
        this.loading = false;
      },
      error: () => {
        this.error = 'Fehler beim Laden der Nutzer.';
        this.loading = false;
      }
    });
  }

  editUser(user: GetAllUsersDto): void {
    this.api.usersGET(user.id!).subscribe({
      next: userDetails => {
        this.editingUser = userDetails;
        this.userForm.patchValue({
          displayName: userDetails.displayName
        });
      },
      error: () => this.error = 'Fehler beim Laden des Nutzers.'
    });
  }

  cancelEdit(): void {
    this.editingUser = null;
    this.userForm.reset();
  }

  submitUser(): void {
    if (this.userForm.invalid || !this.editingUser) return;
    let dto: UpdateUserCommand = new UpdateUserCommand();
    dto.displayName = this.userForm.value.displayName;
    this.api.usersPUT(this.editingUser.id!, dto).subscribe({
      next: () => {
        this.loadUsers();
        this.cancelEdit();
      },
      error: () => this.error = 'Fehler beim Aktualisieren.'
    });
  }

  deleteUser(user: GetAllUsersDto): void {
    if (!confirm(`Nutzer "${user.displayName}" wirklich löschen?`)) return;
    this.api.usersDELETE(user.id!).subscribe({
      next: () => this.loadUsers(),
      error: () => this.error = 'Fehler beim Löschen.'
    });
  }
}
