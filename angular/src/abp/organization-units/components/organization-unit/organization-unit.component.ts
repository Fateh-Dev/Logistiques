import {
  abpAnimations,
  BaseAsyncComponent,
  BaseCrudComponent,
  CleanObjectProperties,
  LocalDataSource,
} from '@abpdz/ng.theme.shared';
import { HttpClient } from '@angular/common/http';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Injector,
  OnInit,
} from '@angular/core';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
import { Injectable } from '@angular/core';
import { BehaviorSubject, merge, Observable } from 'rxjs';
import {
  CollectionViewer,
  DataSource,
  SelectionChange,
} from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { map, tap } from 'rxjs/operators';
import { FormBuilder } from '@angular/forms';
import { EMPTYGUID } from '@abpdz/ng.core';

export interface OrganizationUnit {
  id: string;
  display?: string;
}

@Injectable({
  providedIn: 'root',
})
export class OrganizationUnitService {
  base = '/api/identity/organization-units/';
  constructor(public http: HttpClient) {}
  all(parent?): Observable<OrganizationUnit[]> {
    return this.http
      .get<any>(this.base + 'all', {
        params: CleanObjectProperties({
          maxResultCount: 1000,
          skipCount: 0,
          parent,
        }) as any,
      })
      .pipe(map((k) => k?.items));
  }
  createupdate(item: OrganizationUnit): Observable<OrganizationUnit> {
    return this.http.post<OrganizationUnit>(this.base, item);
  }
}

/**
 * Database for dynamic data. When expanding a node in the tree, the data source will need to fetch
 * the descendants data from the database.
 */
@Injectable({ providedIn: 'root' })
export class DynamicDatabase {
  constructor(private service: OrganizationUnitService) {}
  /** Initial data from database */
  initialData(): Observable<OrganizationUnitFlatNode[]> {
    return this.service
      .all()
      .pipe(
        map((all) => all.map((n) => new OrganizationUnitFlatNode(n, 0, true)))
      );
  }

  getChildren(node: OrganizationUnit): OrganizationUnit[] | undefined {
    return null;
  }

  isExpandable(node: OrganizationUnit): boolean {
    return true;
  }
}

export class DynamicDataSource implements DataSource<OrganizationUnitFlatNode> {
  dataChange = new BehaviorSubject<OrganizationUnitFlatNode[]>([]);

  get data(): OrganizationUnitFlatNode[] {
    return this.dataChange.value;
  }
  set data(value: OrganizationUnitFlatNode[]) {
    this._treeControl.dataNodes = value;
    this.dataChange.next(value);
  }

  constructor(
    private _treeControl: FlatTreeControl<OrganizationUnitFlatNode>,
    private _database: DynamicDatabase,
    private service: OrganizationUnitService,
    private cd: ChangeDetectorRef
  ) {}

  connect(
    collectionViewer: CollectionViewer
  ): Observable<OrganizationUnitFlatNode[]> {
    console.log('collv', collectionViewer);
    this._treeControl.expansionModel.changed.subscribe((change) => {
      if (
        (change as SelectionChange<OrganizationUnitFlatNode>).added ||
        (change as SelectionChange<OrganizationUnitFlatNode>).removed
      ) {
        this.handleTreeControl(
          change as SelectionChange<OrganizationUnitFlatNode>
        );
      }
    });

    return merge(collectionViewer.viewChange, this.dataChange).pipe(
      map(() => this.data)
    );
  }

  disconnect(collectionViewer: CollectionViewer): void {}

  /** Handle expand/collapse behaviors */
  handleTreeControl(change: SelectionChange<OrganizationUnitFlatNode>) {
    if (change.added) {
      change.added.forEach((node) => this.toggleNode(node, true));
    }
    if (change.removed) {
      change.removed
        .slice()
        .reverse()
        .forEach((node) => this.toggleNode(node, false));
    }
  }

  /**
   * Toggle the node, remove from display list
   */
  toggleNode(node: OrganizationUnitFlatNode, expand: boolean) {
    node.isLoading = true;
    const index = this.data.indexOf(node);
    this.service.all(node?.item?.id).subscribe((children) => {
      if (expand) {
        const nodes = children.map(
          (name) =>
            new OrganizationUnitFlatNode(
              name,
              node.level + 1,
              this._database.isExpandable(name)
            )
        );
        this.data.splice(index + 1, 0, ...nodes);
      } else {
        let count = 0;
        for (
          let i = index + 1;
          i < this.data.length && this.data[i].level > node.level;
          i++, count++
        ) {}
        this.data.splice(index + 1, count);
      }

      // notify the change
      this.dataChange.next(this.data);
      node.isLoading = false;
      this.cd.markForCheck();
    });
  }
}

export class DynamicTreeFlatNode<T> {
  constructor(
    public item: T,
    public level = 1,
    public expandable = false,
    public isLoading = false
  ) {}
}
export class OrganizationUnitFlatNode extends DynamicTreeFlatNode<OrganizationUnit> {}

@Component({
  selector: 'app-organization-unit',
  templateUrl: './organization-unit.component.html',
  styleUrls: ['./organization-unit.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: abpAnimations,
})
export class OrganizationUnitComponent
  extends BaseCrudComponent<OrganizationUnit>
  implements OnInit {
  treeControl: FlatTreeControl<OrganizationUnitFlatNode>;

  treeSource: DynamicDataSource;

  getLevel = (node: OrganizationUnitFlatNode) => node.level;

  isExpandable = (node: OrganizationUnitFlatNode) => node.expandable;

  hasChild = (_: number, _nodeData: OrganizationUnitFlatNode) =>
    _nodeData.expandable;

  constructor(
    private httpClient: HttpClient,
    public database: DynamicDatabase,
    private fb: FormBuilder,
    public service: OrganizationUnitService,
    injector: Injector
  ) {
    super(injector);
    this.treeControl = new FlatTreeControl<OrganizationUnitFlatNode>(
      this.getLevel,
      this.isExpandable
    );
    this.treeSource = new DynamicDataSource(
      this.treeControl,
      database,
      this.service,
      this.cd
    );

    database.initialData().subscribe((k) => (this.treeSource.data = k));
    this.dataSource = new LocalDataSource<OrganizationUnit>();
  }
  root: OrganizationUnit[];
  ngOnInit(): void {
    this.editForm = this.fb.group({
      displayName: [],
      id: [],
      parentId: [],
    });
    // this.loading++;
    // this.service.all().subscribe(
    //   (k) => {
    //     this.loading--;
    //   },
    //   (e) => this.asyncError(e)
    // );
  }
  createEdit(e?) {
    if (e == null) {
      e = { id: EMPTYGUID };
    }
    if (e.id == null) {
      e.id = EMPTYGUID;
    }
    this.dialogEdit(e);
  }
  saveItem() {
    this.loading++;
    this.service.createupdate(this.editForm.value).subscribe((k) => {
      this.loading--;
      this.dialogRef?.close();
      this.dialogRef = null;
    });
  }
}
