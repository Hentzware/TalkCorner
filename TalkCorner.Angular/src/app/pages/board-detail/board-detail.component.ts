import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiClient, GetThreadsByBoardIdDto } from '../../api/board-client';

@Component({
  selector: 'app-board-detail',
  templateUrl: './board-detail.component.html',
  styleUrls: ['./board-detail.component.css']
})
export class BoardDetailComponent implements OnInit {
  threads: GetThreadsByBoardIdDto[] = [];
  boardId: string = '';
  loading = false;
  error: string | null = null;

  constructor(private api: ApiClient, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.boardId = params.get('id') ?? '';
      this.loadThreads();
    });
  }

  loadThreads(): void {
    if (!this.boardId) return;
    this.loading = true;
    this.api.board(this.boardId).subscribe({
      next: threads => {
        this.threads = threads;
        this.loading = false;
      },
      error: () => {
        this.error = 'Fehler beim Laden der Threads';
        this.loading = false;
      }
    });
  }
}
