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
import { FuseMapNavigationService } from '../../fuse/services/fuse-map-navigation.service';
import { Store } from '@ngrx/store';
@Component({
  selector: 'fuse-application-layout',
  templateUrl: './fuse-application-layout.component.html',
  styleUrls: [],
})
export class FuseApplicationLayoutComponent
  implements AfterViewInit, OnInit, OnDestroy {
  // required for dynamic component
  ready$: Observable<boolean>;
 
    // Set the private defaults
  fuseConfig: any;
  // Private
  private _unsubscribeAll: Subject<any>;

  constructor(
    @Inject(DOCUMENT) private document: any,

    private _fuseConfigService: FuseConfigService,

    store: Store
  ) {
    // Set the private defaults

    this.ready$ = store.select(selectAppReady);

    this._unsubscribeAll = new Subject();

    // Register the navigation to the service
  }

  ngAfterViewInit(): void {}
  ngOnInit(): void {
    // Subscribe to config changes
    this._fuseConfigService.config
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((config) => {
        this.fuseConfig = config;
      });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
