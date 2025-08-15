import { Component, OnInit } from '@angular/core';
import { ApiClient, GetAllBoardsDto, GetBoardByIdDto } from '../../../api/board-client';
import { AuthService } from '../../../services/auth.service';
import { CreateBoardCommand, UpdateBoardCommand } from '../../../api/board-client';

@Component({
  selector: 'app-admin-boards',
  templateUrl: './admin-boards.component.html',
  styleUrls: ['./admin-boards.component.css']
})
export class AdminBoardsComponent implements OnInit {
  boards: GetAllBoardsDto[] = [];
  loading = false;
  error: string | null = null;
  showDeleteConfirmDialog: GetBoardByIdDto | null = null;

  // Für Board-Dialog:
  showBoardDialog = false;
  boardDialogMode: 'create' | 'edit' = 'create';
  editingBoard: GetBoardByIdDto | null = null;

  constructor(
    private api: ApiClient,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadBoards();
  }

  loadBoards(): void {
    this.loading = true;
    this.api.boardsAll().subscribe({
      next: (boards: GetAllBoardsDto[]) => {
        this.boards = boards;
        this.loading = false;
      },
      error: () => {
        this.error = "Fehler beim Laden der Boards.";
        this.loading = false;
      }
    });
  }

  openBoardDialog(mode: 'create' | 'edit', board?: GetBoardByIdDto): void {
    this.boardDialogMode = mode;
    this.editingBoard = mode === 'edit' && board ? board : null;
    this.showBoardDialog = true;
  }

  onBoardDialogCancel(): void {
    this.showBoardDialog = false;
    this.editingBoard = null;
  }

  onBoardDialogSave(data: { title: string; description: string, sortOrder: number }): void {
    if (this.boardDialogMode === 'edit' && this.editingBoard) {
      const dto = new UpdateBoardCommand({ title: data.title, description: data.description, sortOrder: data.sortOrder });
      this.api.boardsPUT(this.editingBoard.id + '', dto).subscribe({
        next: () => {
          this.loadBoards();
          this.showBoardDialog = false;
          this.editingBoard = null;
        },
        error: () => {
          this.error = "Fehler beim Aktualisieren.";
        }
      });
    } else {
      const dto = new CreateBoardCommand({ title: data.title, description: data.description, sortOrder: data.sortOrder });
      this.api.boardsPOST(dto).subscribe({
        next: () => {
          this.loadBoards();
          this.showBoardDialog = false;
        },
        error: () => {
          this.error = "Fehler beim Anlegen.";
        }
      });
    }
  }

  deleteBoard(board: GetBoardByIdDto): void {
    this.showDeleteConfirmDialog = board;
  }

  onDeleteConfirmed() {
    if (this.showDeleteConfirmDialog) {
      this.api.boardsDELETE(this.showDeleteConfirmDialog.id + '').subscribe({
        next: () => {
          this.loadBoards();
          this.showDeleteConfirmDialog = null;
        },
        error: () => {
          this.error = "Error";
          this.showDeleteConfirmDialog = null;
        }
      });
    }
  }

  onDeleteCanceled(): void {
    this.showDeleteConfirmDialog = null;
  }
}
