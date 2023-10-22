import { Component, OnInit } from '@angular/core';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  /*
  * A injeção de dependência pode ser feita de três formas:
  * 1- No componente de serviço declarando que ele é injetado na raiz do projeto
  * 2- No componente que irá utilizar o serviço
  * 3- No app.module.ts, que tem as configurações gerais
  * Por padrão, vamos adotar a opção 3 neste projeto
  * A linha abaixo, comentada, é a forma 2
  * providers: [EventoService],
  */
})
export class EventosComponent implements OnInit {
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public widthImg: number = 80;
  public marginImg: number = 2;
  public isCollapsed = true;

  private _filtroLista: string = '';

  constructor(private eventoService: EventoService) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    const observer = {
      next: (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {
        console.log('Finalizado');
      }
    }
    this.eventoService.getEventos().subscribe(observer);
  }

  public get filtroLista(){
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string }) =>
        evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }
}
