import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeafletmapviewComponent } from './leafletmapview.component';

describe('LeafletmapviewComponent', () => {
  let component: LeafletmapviewComponent;
  let fixture: ComponentFixture<LeafletmapviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeafletmapviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeafletmapviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
