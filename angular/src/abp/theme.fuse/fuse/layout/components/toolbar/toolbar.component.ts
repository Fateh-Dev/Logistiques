import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { Subject, Observable, combineLatest } from 'rxjs';
import { takeUntil, map, filter } from 'rxjs/operators';

import { FuseConfigService } from '@fuse/services/config.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';

import { Router } from '@angular/router';
import {
  AuthService,
  ApplicationConfiguration,
  setLanguage,
  selectLanguages,
  selectCurrentLanguage,
  selectCurrentCulture,
  SessionStateService,
} from '@abpdz/ng.core';
import { Store } from '@ngrx/store';
import { FuseMapNavigationService } from '../../../services/fuse-map-navigation.service';

// tslint:disable: member-ordering
@Component({
  selector: 'toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ToolbarComponent implements OnInit, OnDestroy {
  horizontalNavbar: boolean;
  rightNavbar: boolean;
  hiddenNavbar: boolean;
  get smallScreen(): boolean {
    return window.innerWidth < 992;
  }

  dropdownLanguages$: Observable<ApplicationConfiguration.Language[]>;

  onChangeLang(cultureName: string) {
    this.session.setLanguage(cultureName);
  }

  // Private
  private _unsubscribeAll: Subject<any>;

  constructor(
    private _fuseConfigService: FuseConfigService,
    private _fuseSidebarService: FuseSidebarService,
    public fuseNavMap: FuseMapNavigationService,
    public authService: AuthService,
    private store: Store,
    private router: Router,
    public session: SessionStateService
  ) {
    // Set the private defaults
    this._unsubscribeAll = new Subject();

    this.dropdownLanguages$ = combineLatest([
      this.session.languages$,
      this.session.selectedCulture$,
    ]).pipe(
      map(([languages, selectedLangCulture]) =>
        languages?.filter(
          (lang) => lang?.cultureName !== selectedLangCulture?.cultureName
        )
      )
    );
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Lifecycle hooks
  // -----------------------------------------------------------------------------------------------------

  /**
   * On init
   */
  ngOnInit(): void {
    // Subscribe to the config changes
    this._fuseConfigService.config
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((settings) => {
        this.horizontalNavbar = settings.layout.navbar.position === 'top';
        this.hiddenNavbar = settings.layout.navbar.hidden === true;
      });
  }

  /**
   * On destroy
   */
  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * Toggle sidebar open
   *
   * @param key
   */
  toggleSidebarOpen(key): void {
    this._fuseSidebarService.getSidebar(key).toggleOpen();
  }

  /**
   * Search
   *
   * @param value
   */
  search(value): void {
    // Do your search here...
  }

  logout(): void {
    this.authService.logout().subscribe(() => {});
  }
}
