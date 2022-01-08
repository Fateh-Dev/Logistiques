import { FuseApplicationLayoutComponent } from '@abpdz/ng.theme.fuse';
import { MaterialApplicationLayoutComponent } from '@abpdz/ng.theme.material';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('@abpdz/ng.account').then((m) =>
        m.AccountModule.forLazy({ redirectUrl: '/' })
      ),
  },
  {
    path: '',
    // component: FuseApplicationLayoutComponent,
    component: MaterialApplicationLayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      {
        path: 'account',
        loadChildren: () =>
          import('@abpdz/ng.account').then((m) =>
            m.AccountModule.forLazy({ redirectUrl: '/' })
          ),
      },
      {
        path: 'identity',
        loadChildren: () =>
          import('@abpdz/ng.identity').then((m) => m.IdentityModule.forLazy()),
      },
      {
        path: 'audit',
        loadChildren: () =>
          import('@abpdz/ng.audit').then((m) => m.AbpDzAuditModule.forLazy()),
      },
      {
        path: 'enums',
        loadChildren: () =>
          import('@abpdz/ng.enums').then((m) => m.EnumsModule.forLazy()),
      },
      {
        path: 'maps',
        loadChildren: () =>
          import('@abpdz/ng.map-view').then((m) => m.MapViewModule.forLazy()),
      },
      {
        path: 'demos',
        loadChildren: () =>
          import('@abpdz/ng.demos').then((m) => m.AbpDzDemosModule.forLazy()),
      },
      {
        path: 'organization-units',
        loadChildren: () =>
          import('@abpdz/ng.organization-units').then((m) =>
            m.OrganizationUnitModule.forLazy()
          ),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
