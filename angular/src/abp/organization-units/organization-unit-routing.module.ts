import { PermissionGuard } from '@abpdz/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationUnitComponent } from './components';

const routes: Routes = [
  {
    canActivate: [PermissionGuard],
    path: 'ou',
    component: OrganizationUnitComponent,
    data: {
      requiredPolicy: 'ABPDZ.OrganizationUnit',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrganizationUnitRoutingModule {}
