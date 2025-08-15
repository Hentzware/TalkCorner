import { Component, OnInit } from '@angular/core';
import { ApiClient, GetAllBoardsDto } from '../../api/board-client';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-boards',
  templateUrl: './boards.component.html'
})
export class BoardsComponent implements OnInit {
  boards: GetAllBoardsDto[] = [];
  loading = false;
  error: string | null = null;

  boardForm: FormGroup;

  constructor(private api: ApiClient, private fb: FormBuilder) {
    this.boardForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.maxLength(250)]],
    });
  }

  ngOnInit(): void {
    this.fetchBoards();
  }

  fetchBoards() {
    this.loading = true;
    this.api.boardsAll().subscribe({
      next: (boards) => {
        this.boards = boards ?? [];
        this.loading = false;
      },
      error: (err) => {
        this.error = typeof err === 'string' ? err : (err?.title ?? 'Fehler beim Laden der Boards.');
        this.loading = false;
      }
    });
  }
}
