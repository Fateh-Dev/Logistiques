import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { FuseNavigationModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';

import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { CoreModule } from '@abpdz/ng.core';
import { NavbarVerticalStyle1Component } from './style-1.component';

@NgModule({
  declarations: [NavbarVerticalStyle1Component],
  imports: [
    MatButtonModule,
    MatIconModule,

    ThemeSharedModule,
    CoreModule,
    FuseSharedModule,
    FuseNavigationModule,
  ],
  exports: [NavbarVerticalStyle1Component],
})
export class NavbarVerticalStyle1Module {}
