import { DynamicLayoutComponent, PermissionGuard } from '@abpdz/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LeafletmapviewComponent } from './leafletmapview/leafletmapview.component';
// import { EnumsListComponent } from './enums-list/enums-list.component';

const routes: Routes = [
  {
    path: '',
    component: LeafletmapviewComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'ABPDZ.Enums',
    },
    // children: [
    //   {
    //     path: '',
    //     canActivate: [PermissionGuard],
    //     data: {
    //       requiredPolicy: 'ABPDZ.Enums',
    //     },
    //     component: LeafletmapviewComponent,
    //   },
    // ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MapViewRoutingModule {}
