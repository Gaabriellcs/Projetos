import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { AvatarModule } from 'primeng/avatar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ButtonModule, MenubarModule , CommonModule, AvatarModule,   ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'ControleGastoFront';

  items: any[] = [];

  ngOnInit() {
    this.items = [
      {
        label: 'Dashboard',
        icon: 'pi pi-home',
        routerLink: ['/dashboard']
      },
      {
        label: 'Relatórios',
        icon: 'pi pi-chart-line',
        items: [
          { label: 'Mensal', icon: 'pi pi-calendar', routerLink: ['/relatorios/mensal'] },
          { label: 'Anual', icon: 'pi pi-chart-bar', routerLink: ['/relatorios/anual'] }
        ]
      },
      {
        label: 'Configurações',
        icon: 'pi pi-cog',
        items: [
          { label: 'Banco', icon: 'pi pi-dollar', routerLink: ['/banco'] },
          { label: 'Fatura', icon: 'pi pi-file', routerLink: ['/faturas'] },
          { label: 'Categoria', icon: 'pi pi-file', routerLink: ['/categoria'] },
        ]
      }
    ];
  }
}