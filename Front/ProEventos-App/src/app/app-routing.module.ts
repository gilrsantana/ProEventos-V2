import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';

import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';

import { ContatosComponent } from './components/contatos/contatos.component';

import { EventosComponent } from './components/eventos/eventos.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';

import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import {authGuard} from "@app/guard/auth.guard";
import {HomeComponent} from "@app/components/home/home.component";


const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'cadastro', component: RegistrationComponent },
    ]
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'user', redirectTo: 'user/perfil' },
      { path: 'user/perfil', component: PerfilComponent},
      { path: 'eventos', redirectTo: 'eventos/lista' },
      {
        path: 'eventos', component: EventosComponent,
        children: [
          { path: 'detalhe/:id', component: EventoDetalheComponent },
          { path: 'detalhe', component: EventoDetalheComponent },
          { path: 'lista', component: EventoListaComponent}
        ]
      },
      { path: 'palestrantes', component: PalestrantesComponent, canActivate: [authGuard] },
      { path: 'contatos', component: ContatosComponent, canActivate: [authGuard] },
      { path: 'dashboard', component: DashboardComponent }
    ]
  },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'cadastro', component: RegistrationComponent },
    ]
  },
  { path: 'home', component: HomeComponent, },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
