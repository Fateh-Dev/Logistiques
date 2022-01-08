import { ModuleWithProviders, NgModule, NgModuleFactory } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationUnitRoutingModule } from './organization-unit-routing.module';
import { CoreModule, LazyModuleFactory } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { OrganizationUnitComponent } from './components/organization-unit/organization-unit.component';
import { MatTreeModule } from '@angular/material/tree';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CdkTreeModule } from '@angular/cdk/tree';
import { AbpTree, AbpTreeNode } from './abp-tree';

@NgModule({
  declarations: [OrganizationUnitComponent, AbpTree, AbpTreeNode],
  imports: [
    CommonModule,
    OrganizationUnitRoutingModule,
    CommonModule,
    ThemeSharedModule,
    MatTableModule,
    MatTreeModule,
    MatPaginatorModule,
    MatProgressBarModule,
    CdkTreeModule,
    CoreModule.forLazy(),
    NgxValidateCoreModule,
  ],
})
export class OrganizationUnitModule {
  static forChild(): ModuleWithProviders<OrganizationUnitModule> {
    return {
      ngModule: OrganizationUnitModule,
      providers: [],
    };
  }
  static forLazy(): NgModuleFactory<OrganizationUnitModule> {
    return new LazyModuleFactory(OrganizationUnitModule.forChild());
  }
}
