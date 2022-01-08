import { Inject, Injectable, InjectionToken } from '@angular/core';
import { ResolveEnd, Router } from '@angular/router';
import { Platform } from '@angular/cdk/platform';
import { BehaviorSubject, Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import { cloneDeep, merge, isEqual } from 'lodash-es';
import { createSelector, Store } from '@ngrx/store';
import { selectTheme, setThemeConfig } from '@abpdz/ng.theme.shared';
import { SubSink } from '@abpdz/ng.core';
import { OnDestroy } from '@angular/core';
import { FuseConfig } from '@fuse/types';
export const selectThemeConfig = createSelector(
  selectTheme,
  (k) => k.themeConfig as FuseConfig
);
export const selectThemeLayout = createSelector(
  selectThemeConfig,
  (k) => k?.layout ?? {}
);
export const selectColorTheme = createSelector(
  selectThemeConfig,
  (k) => k?.colorTheme ?? {}
);

export const selectCustomScrollbars = createSelector(
  selectThemeConfig,
  (k) => k?.customScrollbars ?? {}
);
// Create the injection token for the custom settings
export const FUSE_CONFIG = new InjectionToken('fuseCustomConfig');

@Injectable({
  providedIn: 'root',
})
export class FuseConfigService implements OnDestroy {
  // Private

  private _defaultConfig: any;
  config$: Observable<any>;
  private configValue: any;
  subs = new SubSink();

  /**
   * Constructor
   *
   * @param {Platform} _platform
   * @param {Router} _router
   * @param _config
   */
  constructor(
    private _platform: Platform,
    private _router: Router,
    private store: Store,
    @Inject(FUSE_CONFIG) private _config
  ) {
    // Set the default config from the user provided config (from forRoot)
    this._defaultConfig = _config;

    // Initialize the service
    this._init();
  }
  ngOnDestroy(): void {}

  // -----------------------------------------------------------------------------------------------------
  // @ Accessors
  // -----------------------------------------------------------------------------------------------------

  /**
   * Set and get the config
   */
  set config(value) {
    // Get the value from the behavior subject
    let config = this.configValue;

    // Merge the new config
    config = merge({}, config, value);

    // Notify the observers
    this.updateconfig(config);
  }

  get config(): any | Observable<any> {
    return this.config$;
  }

  /**
   * Get default config
   *
   * @returns {any}
   */
  get defaultConfig(): any {
    return this._defaultConfig;
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Private methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * Initialize
   *
   * @private
   */
  private _init(): void {
    /**
     * Disable custom scrollbars if browser is mobile
     */
    var FuseConf = localStorage.getItem('FUSE_CONFIG');
    if (this._platform.ANDROID || this._platform.IOS) {
      this._defaultConfig.customScrollbars = false;
    }
    let conf = this._defaultConfig;
    if (FuseConf != null) {
      try {
        conf = merge({}, this._defaultConfig, JSON.parse(FuseConf));
      } catch (error) {}
    }
    this.updateconfig(conf, false);
    // Set the config from the default config
    this.config$ = this.store.select(selectThemeConfig);
    this.subs.push(this.config$.subscribe((k) => (this.configValue = k)));

    // Reload the default layout config on every RoutesRecognized event
    // if the current layout config is different from the default one
    this._router.events
      .pipe(filter((event) => event instanceof ResolveEnd))
      .subscribe(() => {
        // todo: update this logic to overide layout for route data
        // if (!isEqual(this.configValue.layout, this._defaultConfig.layout)) {
        //   // Clone the current config
        //   const config = cloneDeep(this.configValue);
        //   // Reset the layout from the default config
        //   config.layout = cloneDeep(this._defaultConfig.layout);
        //   // Set the config
        //   this.updateconfig(config);
        // }
      });
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * Set config
   *
   * @param value
   * @param {{emitEvent: boolean}} opts
   */
  setConfig(value, opts = { emitEvent: true }): void {
    // Get the value from the behavior subject
    let config = this.configValue;

    // Merge the new config
    config = merge({}, config, value);

    // If emitEvent option is true...
    if (opts.emitEvent === true) {
      // Notify the observers
      this.updateconfig(config);
    }
  }

  /**
   * Get config
   *
   * @returns {Observable<any>}
   */
  getConfig(): Observable<any> {
    return this.store.select(selectThemeConfig);
  }

  /**
   * Reset to the default config
   */
  resetToDefaults(): void {
    // Set the config from the default config
    this.updateconfig(cloneDeep(this._defaultConfig));
  }
  updateconfig(data, save = true) {
    if (save) {
      localStorage.setItem('FUSE_CONFIG', JSON.stringify(data));
    }
    this.store.dispatch(setThemeConfig({ data }));
  }
}
