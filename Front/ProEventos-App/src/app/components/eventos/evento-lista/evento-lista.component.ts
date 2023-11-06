import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss'],
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
export class EventoListaComponent {
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public widthImg = 80;
  public marginImg = 2;
  public isCollapsed = true;
  public modalRef?: BsModalRef;
  public message?: string;
  public eventoId = 0;
  private _filtroLista = '';

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  // eslint-disable-next-line @angular-eslint/use-lifecycle-interface
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
        this.toastr.error(
          `Erro ao carregar os eventos. ${error.message}`,
          'Erro!'
        );
      },
      complete: () => {
        this.spinner.hide();
      },
    };
    this.eventoService.getEventos().subscribe(observer);
  }

  public get filtroLista() {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista
      ? this.filtrarEventos(this.filtroLista)
      : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string }) =>
        evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  openModal(event: Event, template: TemplateRef<unknown>, eventoId: number) {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(id: number): void {
    this.modalRef?.hide();
    this.spinner.show();
    this.deleteEvento(id);
    // this.message = 'Confirmed!';
    // this.modalRef?.hide();
    // this.toastr.success('O Evento foi removido com sucesso!', 'Removido');
    // this.toastr.info('O Evento foi removido com sucesso!', 'Removido');
    // this.toastr.warning('O Evento foi removido com sucesso!', 'Removido');
    // this.toastr.show('O Evento foi removido com sucesso!', 'Removido');
    // this.toastr.show('O Evento foi removido com sucesso!', 'Removido', {timeOut: 2000});
  }

  public deleteEvento(id: number): void {
    this.eventoId = id;
    this.eventoService.deleteEvento(id).subscribe(
      (result: boolean) => {
        if(result === true) {
          this.toastr.success('O Evento foi removido com sucesso!', 'Removido');
          this.spinner.hide();
          this.getEventos();
        }
      },
      (error: Error) => {
        this.toastr.error(`Erro ao tentar remover o Evento ${id}: ${error}`, 'Erro');
      },
      () => {
        this.modalRef?.hide();
      }
    );
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void {
    this.router.navigate([`eventos/detalhe/${id}`]);
  }
}
