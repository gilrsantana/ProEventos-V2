import { Component, OnInit, TemplateRef } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { EventoService } from '../../services/evento.service';
import { Evento } from '../../models/Evento';

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
  public widthImg = 80;
  public marginImg = 2;
  public isCollapsed = true;
  modalRef?: BsModalRef;
  message?: string;

  private _filtroLista = '';

  constructor(private eventoService: EventoService, 
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) {}

  ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }

  public getEventos(): void {
    const observer = {
      next: (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      error: (error: Error) => {
        this.spinner.hide();
        this.toastr.error(`Erro ao carregar os eventos. ${error.message}`, 'Erro!');
      },
      complete: () => {
        this.spinner.hide();
      }
    }
    this.eventoService.getEventos().subscribe(observer);
  }

  public get filtroLista(){
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? 
      this.filtrarEventos(this.filtroLista) : 
      this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string }) =>
        evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  openModal(template: TemplateRef<unknown>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef?.hide();
    this.toastr.success('O Evento foi removido com sucesso!', 'Removido');
    // this.toastr.info('O Evento foi removido com sucesso!', 'Removido');
    // this.toastr.warning('O Evento foi removido com sucesso!', 'Removido');
    // this.toastr.show('O Evento foi removido com sucesso!', 'Removido');
    // this.toastr.show('O Evento foi removido com sucesso!', 'Removido', {timeOut: 2000});
  }
 
  decline(): void {
    this.message = 'Declined!';
    this.modalRef?.hide();
  }
}
