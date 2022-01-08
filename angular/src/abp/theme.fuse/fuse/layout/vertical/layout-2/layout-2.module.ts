import { CoreModule } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSidebarModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';
import {
  ContentModule,
  FooterModule,
  NavbarModule,
  QuickPanelModule,
  ToolbarModule,
} from '../../components';
import { VerticalLayout2Component } from './layout-2.component';

@NgModule({
  declarations: [VerticalLayout2Component],
  imports: [
    RouterModule,

    FuseSharedModule,
    FuseSidebarModule,

    ContentModule,
    FooterModule,
    NavbarModule,
    QuickPanelModule,
    ToolbarModule,
    CoreModule,
    ThemeSharedModule,
  ],
  exports: [VerticalLayout2Component],
})
export class VerticalLayout2Module {}
