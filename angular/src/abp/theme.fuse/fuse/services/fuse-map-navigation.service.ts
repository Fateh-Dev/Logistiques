import { Injectable, TrackByFunction } from '@angular/core';
import { RoutesService, ABP, LocalizationService } from '@abpdz/ng.core';
import { Observable } from 'rxjs';
import { FuseNavigation } from '@fuse/types';
import { map, shareReplay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class FuseMapNavigationService {
  navigation$: Observable<FuseNavigation[]>;
  trackByFn: TrackByFunction<any> = (_, item) => item.name;
  constructor(
    public readonly routes: RoutesService,
    private translate: LocalizationService
  ) {
    this.navigation$ = this.routes.visible$.pipe(
      map((k) => (k || []).map((e) => this.map(e))),
      shareReplay()
    );
  }
  isDropdown(node): boolean {
    return this.routes.hasChildren(node.name);
  }

  map(a: any, depth = 0): FuseNavigation {
    if (depth === 8) {
      return null;
    }
    return {
      id: a.name,
      title: this.translate.instant(a.name),
      type: this.isDropdown(a)
        ? depth == 0
          ? 'group'
          : 'collapsable'
        : 'item',
      icon: a.iconClass,
      // hidden?: boolean;
      url: a.path,
      classes: a.classes,
      function: a.function,
      children: (a.children ?? []).map((e) => this.map(e, depth + 1)),
    };
  }
}
