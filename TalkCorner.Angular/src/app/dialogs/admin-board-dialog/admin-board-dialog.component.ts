import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

export type BoardDialogMode = 'create' | 'edit';

@Component({
  selector: 'app-admin-board-dialog',
  templateUrl: './admin-board-dialog.component.html',
  styleUrls: ['./admin-board-dialog.component.css']
})
export class AdminBoardDialogComponent implements OnInit {
  @Input() mode: BoardDialogMode = 'create';
  @Input() initialTitle: string = '';
  @Input() initialDescription: string = '';
  @Input() initialSortOrder: number = 0;
  @Output() cancel = new EventEmitter<void>();
  @Output() save = new EventEmitter<{ title: string; description: string, sortOrder: number }>();

  boardForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.boardForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.maxLength(250)]],
      sortOrder: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.boardForm.patchValue({
      title: this.initialTitle,
      description: this.initialDescription,
      sortOrder: this.initialSortOrder,
    });
  }

  onSave() {
    if (this.boardForm.valid) {
      this.save.emit(this.boardForm.value);
    }
  }

  onCancel() {
    this.cancel.emit();
  }
}
