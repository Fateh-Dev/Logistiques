import { APP_INITIALIZER, ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { eLayoutType, RoutesService } from '@abpdz/ng.core';
import { eThemeSharedRouteNames } from '@abpdz/ng.theme.shared';

export function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: 'maps',
        name: 'AbpDz::Maps',
        parentName: eThemeSharedRouteNames.Administration,
        requiredPolicy: 'ABPDZ.Enums',
        iconClass: 'map',
        layout: eLayoutType.application,
        order: 20,
      },
    ]);
  };
}

@NgModule({
  declarations: [],
  imports: [CommonModule],
})
export class MapViewConfigModule {
  static forRoot(): ModuleWithProviders<MapViewConfigModule> {
    return {
      ngModule: MapViewConfigModule,
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
