// tslint:disable: variable-name
import { eLayoutType, selectAppReady } from '@abpdz/ng.core';
import {
  AfterViewInit,
  Component,
  OnDestroy,
  Inject,
  OnInit,
  ChangeDetectionStrategy,
} from '@angular/core';
import { fromEvent, Observable, Subject } from 'rxjs';
import { debounceTime, map, shareReplay, takeUntil } from 'rxjs/operators';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { Platform } from '@angular/cdk/platform';
import { DOCUMENT } from '@angular/common';
import { FuseConfigService } from '@fuse/services/config.service';
import { FuseMapNavigationService } from '../fuse/services/fuse-map-navigation.service';
import { Store } from '@ngrx/store';
@Component({
  selector: 'app-fuse-root',
  template: '<router-outlet *ngIf="ready$ | async"><router-outlet>',
})
export class FuseRootComponent implements AfterViewInit, OnInit, OnDestroy {
  // required for dynamic component
  ready$: Observable<boolean>;

  // Private

  constructor(private _platform: Platform, store: Store) {
    // Set the private defaults

    this.ready$ = store.select(selectAppReady);
    // Register the navigation to the service
  }

  ngAfterViewInit(): void {}
  ngOnInit(): void {
    // Subscribe to config changes
  }

  ngOnDestroy(): void {}
}
