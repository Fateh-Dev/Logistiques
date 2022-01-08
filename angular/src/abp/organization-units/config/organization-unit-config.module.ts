import { APP_INITIALIZER, ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { eLayoutType, RoutesService } from '@abpdz/ng.core';
import { eThemeSharedRouteNames } from '@abpdz/ng.theme.shared';

export function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/organization-units/ou',
        name: 'AbpDz::OrganizationUnit',
        parentName: eThemeSharedRouteNames.Administration,
        requiredPolicy: 'AbpIdentity.Users',
        iconClass: 'account_tree',
        layout: eLayoutType.application,
        order: 10,
      },
    ]);
  };
}

@NgModule({
  declarations: [],
  imports: [CommonModule],
})
export class OrganizationUnitConfigModule {
  static forRoot(): ModuleWithProviders<OrganizationUnitConfigModule> {
    return {
      ngModule: OrganizationUnitConfigModule,
      providers: [
        {
          provide: APP_INITIALIZER,
          useFactory: configureRoutes,
          deps: [RoutesService],
          multi: true,
        },
      ],
    };
  }
}
