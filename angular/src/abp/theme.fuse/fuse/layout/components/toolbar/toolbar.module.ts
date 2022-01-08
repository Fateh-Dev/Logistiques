import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';

import { FuseSearchBarModule, FuseShortcutsModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';

import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { CoreModule } from '@abpdz/ng.core';
import { ToolbarComponent } from './toolbar.component';

@NgModule({
  declarations: [ToolbarComponent],
  imports: [
    RouterModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatToolbarModule,
    ThemeSharedModule,
    CoreModule,
    FuseSharedModule,
    FuseSearchBarModule,
    FuseShortcutsModule,
  ],
  exports: [ToolbarComponent],
})
export class ToolbarModule {}
