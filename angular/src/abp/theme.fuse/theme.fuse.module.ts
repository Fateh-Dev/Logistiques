import { CoreModule } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import {
  FuseProgressBarModule,
  FuseSidebarModule,
  FuseThemeOptionsModule,
} from '@fuse/components';
import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { EffectsModule } from '@ngrx/effects';
import { ToastrModule } from 'ngx-toastr';
import { FuseApplicationLayoutComponent } from './components/fuse-application-layout/fuse-application-layout.component';
import { FuseRootComponent } from './components/fuse-root.component';
import { fuseConfig } from './fuse/fuse-config';
import { FuseLayoutModule } from './fuse/layout/layout.module';
import { FuseEffects } from './ngrx/fuse.effects';
const components = [FuseRootComponent, FuseApplicationLayoutComponent];
@NgModule({
  declarations: [...components],
  imports: [
    CommonModule,
    RouterModule,
    ThemeSharedModule,

    EffectsModule.forFeature([FuseEffects]),
    FuseLayoutModule,
    FuseModule.forRoot(fuseConfig),
    CoreModule,
    FuseProgressBarModule,
    FuseSharedModule,
    FuseSidebarModule,
    FuseThemeOptionsModule,
  ],
  exports: [...components],
  providers: [],
})
export class BaseThemeFuseModule {}
@NgModule({
  exports: [BaseThemeFuseModule],
  imports: [],
})
export class RootThemeFuseModule {
  constructor() {}
}

@NgModule({
  exports: [BaseThemeFuseModule],
  imports: [BaseThemeFuseModule],
  providers: [],
})
export class ThemeFuseModule {
  static forRoot(): ModuleWithProviders<RootThemeFuseModule> {
    return {
      ngModule: RootThemeFuseModule,
      providers: [],
    };
  }
}
