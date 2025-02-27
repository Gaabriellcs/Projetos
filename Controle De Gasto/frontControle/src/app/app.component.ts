import { ChangeDetectorRef, Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { AvatarModule } from 'primeng/avatar';
import { filter, Observable } from 'rxjs';
import { LoadingService } from '../services/loading.service';
import { ProgressSpinner } from 'primeng/progressspinner';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ButtonModule, MenubarModule , CommonModule, AvatarModule, ProgressSpinner],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [
  ]
})
export class AppComponent {
  private noMenuRoutes: string[] = ['/cadastro', '/login']; // Rota sem menu
  currentRoute: string = '';
  title = 'ControleGastoFront';
  
  items: any[] = [];
  loading$: Observable<boolean>;

  constructor(private router: Router, private activatedRoute: ActivatedRoute, private loadingService: LoadingService,   private cdr: ChangeDetectorRef) {
    this.loading$ = this.loadingService.loading$;
    
    this.loading$.subscribe(); 
  }

  
  ngOnInit() {
    
    this.router.events
    .pipe(filter(event => event instanceof NavigationEnd))
    .subscribe(() => {
      this.currentRoute = this.router.url;
    });

    
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
          { label: 'Categoria', icon: 'pi pi-align-center', routerLink: ['/categoria'] },
          { label: 'Conciliação', icon: 'pi pi-copy', routerLink: ['/conciliacao'] },
        ]
      }
    ];
  }
 
 
  ngAfterViewChecked() {
    this.cdr.detectChanges();
  }

  isRouteWithNoMenu(): boolean {
    return this.noMenuRoutes.includes(this.currentRoute);
  }

  logOff(){
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}