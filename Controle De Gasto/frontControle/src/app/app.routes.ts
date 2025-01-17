import { Routes } from '@angular/router';
import { BancoComponent } from './banco/banco.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FaturasComponent } from './faturas/faturas.component';

export const routes: Routes = [

    
        { path: 'banco', component: BancoComponent },
        { path: 'dashboard', component: DashboardComponent },
        { path: 'faturas', component: FaturasComponent },
];

