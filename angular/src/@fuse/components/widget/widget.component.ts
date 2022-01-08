import {
  AfterContentInit,
  ChangeDetectionStrategy,
  Component,
  ContentChildren,
  ElementRef,
  HostBinding,
  Input,
  QueryList,
  Renderer2,
  ViewEncapsulation,
} from '@angular/core';
import { FuseWidgetToggleDirective } from './widget-toggle.directive';

@Component({
  selector: 'fuse-widget',
  templateUrl: './widget.component.html',
  styleUrls: ['./widget.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FuseWidgetComponent implements AfterContentInit {
  @HostBinding('class.flipped')
  flipped = false;
  @Input()
  id;

  @ContentChildren(FuseWidgetToggleDirective, { descendants: true })
  toggleButtons: QueryList<FuseWidgetToggleDirective>;

  /**
   * Constructor
   *
   * @param {ElementRef} _elementRef
   * @param {Renderer2} _renderer
   */
  constructor(private _elementRef: ElementRef, private _renderer: Renderer2) {}

  // -----------------------------------------------------------------------------------------------------
  // @ Lifecycle hooks
  // -----------------------------------------------------------------------------------------------------

  /**
   * After content init
   */
  ngAfterContentInit(): void {
    // Listen for the flip button click
    setTimeout(() => {
      this.toggleButtons.forEach((flipButton) => {
        this._renderer.listen(
          flipButton.elementRef.nativeElement,
          'click',
          (event) => {
            event.preventDefault();
            event.stopPropagation();
            this.toggle();
          }
        );
      });
    });
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * Toggle the flipped status
   */
  toggle(): void {
    this.flipped = !this.flipped;
  }
}
