import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-delete-confirm-dialog',
  templateUrl: './delete-confirm-dialog.component.html',
})
export class DeleteConfirmDialogComponent {
  @Input() title: string = "Confirm delete";
  @Input() message: string = "Are you sure you want to delete it?";
  @Input() confirmText: string = "Delete";
  @Input() cancelText: string = "Cancel";

  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();
}
