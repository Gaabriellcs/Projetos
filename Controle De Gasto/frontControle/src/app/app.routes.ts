import { Routes } from '@angular/router';
import { BancoComponent } from './banco/banco.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FaturasComponent } from './faturas/faturas.component';
import { CategoriaComponent } from './categoria/categoria.component';
import { CadastroUsuarioComponent } from './cadastro-usuario/cadastro-usuario.component';
import { LoginComponent } from './login/login.component';
import { ConciliacaoComponent } from './conciliacao/conciliacao.component';
import { authGuard } from './guard/auth.guard';

export const routes: Routes = [
        { path: '', component: DashboardComponent, canActivate: [authGuard]  },
        { path: 'banco', component: BancoComponent, canActivate: [authGuard] },
        { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
        { path: 'faturas', component: FaturasComponent, canActivate: [authGuard] },
        { path: 'categoria', component: CategoriaComponent, canActivate: [authGuard] },
        { path: 'cadastro', component: CadastroUsuarioComponent },
        { path: 'login', component: LoginComponent },
        { path: 'conciliacao', component: ConciliacaoComponent, canActivate: [authGuard] },
];

