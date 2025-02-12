import { Routes } from '@angular/router';
import { BancoComponent } from './banco/banco.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FaturasComponent } from './faturas/faturas.component';
import { CategoriaComponent } from './categoria/categoria.component';
import { CadastroUsuarioComponent } from './cadastro-usuario/cadastro-usuario.component';

export const routes: Routes = [
        { path: 'banco', component: BancoComponent },
        { path: 'dashboard', component: DashboardComponent },
        { path: 'faturas', component: FaturasComponent },
        { path: 'categoria', component: CategoriaComponent },
        { path: 'cadastro', component: CadastroUsuarioComponent },
];

