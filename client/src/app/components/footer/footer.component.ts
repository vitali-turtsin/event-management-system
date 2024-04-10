import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent {
  infoByColumn: {
    htmlColumnLabel?: string;
    items: { htmlLabel: string; linkPath?: string }[];
  }[] = [
    {
      htmlColumnLabel: 'Curabitur',
      items: [
        { htmlLabel: 'Emauris', linkPath: '/emauris' },
        { htmlLabel: 'Khringilla', linkPath: '/khringilla' },
        { htmlLabel: 'Oin magna sem', linkPath: '/oin-magna-sem' },
        { htmlLabel: 'Sed consequat', linkPath: '/sed-consequat' },
      ],
    },
    {
      htmlColumnLabel: 'Fusce',
      items: [
        { htmlLabel: 'Emauris', linkPath: '/emauris' },
        { htmlLabel: 'Khringilla', linkPath: '/khringilla' },
        { htmlLabel: 'Oin magna sem', linkPath: '/oin-magna-sem' },
        { htmlLabel: 'Sed consequat', linkPath: '/sed-consequat' },
      ],
    },
    {
      htmlColumnLabel: 'Kontakt',
      items: [
        { htmlLabel: '<b>Peakontor: Tallinas</b>' },
        { htmlLabel: 'Väike-Ameerika 1, 11415 Tallinn' },
        { htmlLabel: 'Telefon: 605 4450' },
        { htmlLabel: 'Faks: 605 3186' },
      ],
    },
    {
      htmlColumnLabel: '&nbsp;',
      items: [
        { htmlLabel: '<b>Harjukontor: Võrus</b>' },
        { htmlLabel: 'Oja tn 7 (Külastusaadress)' },
        { htmlLabel: 'Telefon: 6053330' },
        { htmlLabel: 'FaksÖ 6053155' },
      ],
    },
  ];
}
