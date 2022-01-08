import { ModuleWithProviders, NgModule, NgModuleFactory } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeafletmapviewComponent } from './leafletmapview/leafletmapview.component';
import { MapViewRoutingModule } from './map-view-routing.module';
import { CoreModule, LazyModuleFactory } from '@abpdz/ng.core';
import { ThemeSharedModule } from '@abpdz/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [LeafletmapviewComponent],
  imports: [
    CommonModule,
    MapViewRoutingModule,
    ThemeSharedModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    CoreModule.forLazy(),
    NgxValidateCoreModule,
  ],
})
export class MapViewModule {
  static forChild(): ModuleWithProviders<MapViewModule> {
    return {
      ngModule: MapViewModule,
      providers: [],
    };
  }
  static forLazy(): NgModuleFactory<MapViewModule> {
    return new LazyModuleFactory(MapViewModule.forChild());
  }
}
