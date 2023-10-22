import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { NavComponent } from './nav/nav.component';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { EventoService } from './services/evento.service';

@NgModule({
  declarations: [	
    AppComponent,
    EventosComponent,
    PalestrantesComponent,
    NavComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    FormsModule
  ],
  /*
  * A injeção de dependência pode ser feita de três formas:
  * 1- No componente de serviço declarando que ele é injetado na raiz do projeto
  * 2- No componente que irá utilizar o serviço
  * 3- No app.module.ts, que tem as configurações gerais
  * Por padrão, vamos adotar a opção 3 neste projeto
  * A linha abaixo é a forma 3
  */
  providers: [EventoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
