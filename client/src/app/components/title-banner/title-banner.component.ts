import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-title-banner',
  standalone: true,
  imports: [],
  templateUrl: './title-banner.component.html',
  styleUrl: './title-banner.component.scss',
})
export class TitleBannerComponent {
  @Input() title: string = '';
}
