import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';

import { FuseSharedModule } from '@fuse/shared.module';
import { FooterComponent } from './footer.component';
import { CoreModule } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';

@NgModule({
  declarations: [FooterComponent],
  imports: [
    RouterModule,

    MatButtonModule,
    MatIconModule,
    MatToolbarModule,

    FuseSharedModule,
    CoreModule,
    ThemeSharedModule,
  ],
  exports: [FooterComponent],
})
export class FooterModule {}
