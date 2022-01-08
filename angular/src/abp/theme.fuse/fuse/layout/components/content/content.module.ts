import { CoreModule } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';
import { ContentComponent } from './content.component';

@NgModule({
  declarations: [ContentComponent],
  imports: [RouterModule, FuseSharedModule, CoreModule, ThemeSharedModule],
  exports: [ContentComponent],
})
export class ContentModule {}
