import { Directive, Input, TemplateRef, ViewContainerRef, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[tcHasAnyRole]'
})
export class HasAnyRoleDirective implements OnInit, OnDestroy {
  private roles: string[] = [];
  private elseTemplateRef: TemplateRef<any> | null = null;
  private sub?: Subscription;

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private authService: AuthService
  ) {}

  @Input() set tcHasAnyRole(roles: string | string[]) {
    this.roles = Array.isArray(roles) ? roles : [roles];
    this.updateView();
  }

  @Input() set tcHasAnyRoleElse(templateRef: TemplateRef<any> | null) {
    this.elseTemplateRef = templateRef;
    this.updateView();
  }

  ngOnInit() {
    this.sub = this.authService.authStatus$.subscribe(() => {
      this.updateView();
    });
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }

  private updateView() {
    const currentRoles = this.authService.getRolesFromToken() ?? [];
    const hasRole = this.roles.some(role => currentRoles.includes(role));
    this.viewContainer.clear();
    if (hasRole) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else if (this.elseTemplateRef) {
      this.viewContainer.createEmbeddedView(this.elseTemplateRef);
    }
  }
}
