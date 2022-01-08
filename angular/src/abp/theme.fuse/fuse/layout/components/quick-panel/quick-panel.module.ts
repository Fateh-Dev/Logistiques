import { CoreModule } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { NgModule } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { FuseSharedModule } from '@fuse/shared.module';
import { QuickPanelComponent } from './quick-panel.component';

@NgModule({
  declarations: [QuickPanelComponent],
  imports: [
    MatDividerModule,
    MatListModule,
    MatSlideToggleModule,

    FuseSharedModule,
    CoreModule,
    ThemeSharedModule,
  ],
  exports: [QuickPanelComponent],
})
export class QuickPanelModule {}
