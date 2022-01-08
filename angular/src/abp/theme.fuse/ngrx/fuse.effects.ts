import { Platform } from '@angular/cdk/platform';
import { Injectable } from '@angular/core';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { FuseConfigService } from '@fuse/services/config.service';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs/operators';
import { FuseMapNavigationService } from '../fuse/services';

@Injectable({
  providedIn: 'root',
})
export class FuseEffects {
  navigation: any;
  constructor(
    private actions$: Actions,
    private _platform: Platform,
    private _fuseNavigationService: FuseNavigationService,

    private _fuseMapNavigationService: FuseMapNavigationService,
    private _fuseConfigService: FuseConfigService
  ) {
    // Add is-mobile class to the body if the platform is mobile
    if (this._platform.ANDROID || this._platform.IOS) {
      document.body.classList.add('is-mobile');
    }
    this.navigation = [];
    this._fuseNavigationService.register('main', []);
    this._fuseNavigationService.setCurrentNavigation('main');
    this._fuseMapNavigationService.navigation$.subscribe((k) => {});
  }

  navigation$ = createEffect(
    () =>
      this._fuseMapNavigationService.navigation$.pipe(
        tap((k) => {
          this.navigation = k ?? [];
          this._fuseNavigationService.unregister('main');
          // Register the navigation to the service
          this._fuseNavigationService.register('main', this.navigation);

          // Set the main navigation as our current navigation
          this._fuseNavigationService.setCurrentNavigation('main');
        })
      ),
    {
      dispatch: false,
    }
  );

  fuseConfig$ = createEffect(
    () =>
      this._fuseConfigService.config$.pipe(
        tap((config) => {
          // Boxed
          if (config?.layout?.width === 'boxed') {
            document.body.classList.add('boxed');
          } else {
            document.body.classList.remove('boxed');
          }

          // Color theme - Use normal for loop for IE11 compatibility
          // tslint:disable-next-line: prefer-for-of
          for (let i = 0; i < document.body.classList.length; i++) {
            const className = document.body.classList[i];

            if (className.startsWith('theme-')) {
              document.body.classList.remove(className);
            }
          }

          document.body.classList.add(config?.colorTheme);
        })
      ),
    {
      dispatch: false,
    }
  );
}
