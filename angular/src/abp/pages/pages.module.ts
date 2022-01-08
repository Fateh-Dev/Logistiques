import { ModuleWithProviders, NgModule, NgModuleFactory } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AbpDzPagesRoutingModule } from './pages-routing.module';
import { CoreModule, LazyModuleFactory } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AbpDzPagesRoutingModule,
    CommonModule,
    ThemeSharedModule,
    CoreModule.forLazy(),
    NgxValidateCoreModule,
  ],
})
export class AbpDzPagesModule {
  static forChild(): ModuleWithProviders<AbpDzPagesModule> {
    return {
      ngModule: AbpDzPagesModule,
      providers: [],
    };
  }
  static forLazy(): NgModuleFactory<AbpDzPagesModule> {
    return new LazyModuleFactory(AbpDzPagesModule.forChild());
  }
}
