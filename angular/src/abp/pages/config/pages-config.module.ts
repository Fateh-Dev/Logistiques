import { APP_INITIALIZER, ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { eLayoutType, RoutesService } from '@abpdz/ng.core';

export function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/pages/pings',
        name: 'Pings',
        parentName: 'Pages',
        iconClass: 'caller',
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
export class AbpDzPagesConfigModule {
  static forRoot(): ModuleWithProviders<AbpDzPagesConfigModule> {
    return {
      ngModule: AbpDzPagesConfigModule,
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
