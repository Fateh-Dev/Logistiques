import { CORE_OPTIONS, ABP } from '@abpdz/ng.core';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';

@Component({
  selector: 'footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FooterComponent {
  /**
   * Constructor
   */
  constructor(@Inject(CORE_OPTIONS) public options: ABP.Root) {}
}
