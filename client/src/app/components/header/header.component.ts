import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    AutoCompleteModule,
    RouterModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  links: {
    label: string;
    path: string;
    selected: boolean;
  }[] = [
    { label: 'Avaleht', path: '/', selected: true },
    {
      label: 'Ürituse lisamine',
      path: '/lisa-yritus',
      selected: false,
    },
  ];

  constructor(private router: Router) {}

  ngOnInit() {
    this.links.forEach((link) => {
      link.selected = this.router.url === link.path;
    });

    this.router.events.subscribe(() => {
      this.links.forEach((link) => {
        link.selected = this.router.url === link.path;
      });
    });
  }
}
