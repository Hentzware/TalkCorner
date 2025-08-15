import { Directive, Input, TemplateRef, ViewContainerRef, OnInit, OnDestroy } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { Subscription } from "rxjs";

@Directive({
  selector: '[tcHasAllRole]'
})
export class HasAllRoleDirective implements OnInit, OnDestroy {
  private roles: string[] = [];
  private elseTemplateRef: TemplateRef<any> | null = null;
  private sub?: Subscription;

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private authService: AuthService
  ) {}

  @Input() set tcHasAllRole(roles: string | string[]) {
    this.roles = Array.isArray(roles) ? roles : [roles];
    this.updateView();
  }

  @Input() set tcHasAllRoleElse(templateRef: TemplateRef<any> | null) {
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
    const hasAllRoles = this.roles.length > 0 && this.roles.every(role => currentRoles.includes(role));
    this.viewContainer.clear();
    if (hasAllRoles) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else if (this.elseTemplateRef) {
      this.viewContainer.createEmbeddedView(this.elseTemplateRef);
    }
  }
}
