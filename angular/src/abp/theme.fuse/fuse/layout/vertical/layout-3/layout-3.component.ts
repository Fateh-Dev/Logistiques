import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';

import { FuseConfigService } from '@fuse/services/config.service';
import { selectCurrentCulture } from '@abpdz/ng.core';
import { Store } from '@ngrx/store';

@Component({
  selector: 'vertical-layout-3',
  templateUrl: './layout-3.component.html',
  styleUrls: ['./layout-3.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VerticalLayout3Component implements OnInit, OnDestroy {
  fuseConfig: any;

  // Private
  private _unsubscribeAll: Subject<any>;
  isRightToLeft$;
  /**
   * Constructor
   *
   * @param {FuseConfigService} _fuseConfigService
   */
  constructor(
    private _fuseConfigService: FuseConfigService,
    private store: Store
  ) {
    // Set the private defaults
    this._unsubscribeAll = new Subject();
    this.isRightToLeft$ = this.store
      .select(selectCurrentCulture)
      .pipe(map((k) => k.isRightToLeft));
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Lifecycle hooks
  // -----------------------------------------------------------------------------------------------------

  /**
   * On init
   */
  ngOnInit(): void {
    // Subscribe to config changes
    this._fuseConfigService.config
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((config) => {
        this.fuseConfig = config;
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
}
