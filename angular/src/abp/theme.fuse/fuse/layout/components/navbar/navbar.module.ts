import { CoreModule } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { NgModule } from '@angular/core';

import { FuseSharedModule } from '@fuse/shared.module';
import { NavbarHorizontalStyle1Module } from './horizontal/style-1/style-1.module';
import { NavbarComponent } from './navbar.component';
import { NavbarVerticalStyle1Module } from './vertical/style-1/style-1.module';
import { NavbarVerticalStyle2Module } from './vertical/style-2/style-2.module';

@NgModule({
  declarations: [NavbarComponent],
  imports: [
    FuseSharedModule,

    NavbarHorizontalStyle1Module,
    NavbarVerticalStyle1Module,
    NavbarVerticalStyle2Module,
    CoreModule,
    ThemeSharedModule,
  ],
  exports: [NavbarComponent],
})
export class NavbarModule {}
