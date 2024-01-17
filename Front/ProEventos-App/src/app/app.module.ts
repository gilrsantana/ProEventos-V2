import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';


import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';

import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { NavComponent } from './shared/nav/nav.component';

import {AccountService} from "@app/services/account.service";
import { EventoService } from './services/evento.service';
import { LoteService } from './services/lote.service';

import {JwtInterceptor} from "@app/interceptors/jwt.interceptor";

import { DateTimeFormatPipe } from './helpers/date-time-format.pipe';
import { TituloComponent } from './shared/titulo/titulo.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { DateTimePipeBrPipe } from './helpers/date-time-pipe-br.pipe';
import { DatePipeBrPipe } from './helpers/date-pipe-br.pipe';

defineLocale('pt-br', ptBrLocale);
registerLocaleData(localePt);

@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    PalestrantesComponent,
    NavComponent,
    DateTimeFormatPipe,
    TituloComponent,
    ContatosComponent,
    DashboardComponent,
    PerfilComponent,
    EventoDetalheComponent,
    EventoListaComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    DateTimePipeBrPipe,
    DatePipeBrPipe,
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: false,
      progressBar: true,
      progressAnimation: 'decreasing'
    }),
    NgxSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    NgxMaskDirective,
    NgxMaskPipe,
    BsDatepickerModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  /*
  * A injeção de dependência pode ser feita de três formas:
  * 1- No componente de serviço declarando que ele é injetado na raiz do projeto
  * 2- No componente que irá utilizar o serviço
  * 3- No app.module.ts, que tem as configurações gerais
  * Por padrão, vamos adotar a opção 3 neste projeto
  * A linha abaixo é a forma 3
  */
 providers: [
   EventoService,
   LoteService,
   AccountService,
   provideNgxMask(),
   { provide: LOCALE_ID, useValue: 'pt-BR' },
   { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
 ],
 bootstrap: [AppComponent],
 schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
