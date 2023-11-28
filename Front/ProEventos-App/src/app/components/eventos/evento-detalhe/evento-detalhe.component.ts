import {Component, OnInit, TemplateRef} from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { Lote } from '@app/models/Lote';
import { EventoService } from '@app/services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LoteService } from '@app/services/lote.service';
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})


export class EventoDetalheComponent implements OnInit{

  form!: FormGroup;
  evento = {} as Evento;
  eventoId = 0;
  minTemaLength = 3;
  maxTemaLength = 50;
  maxQtdPessoas = 120000;
  telefone = '';
  telMask= '(00) 0000-00000'
  modoSalvar = 'post';
  modalRef: BsModalRef = {} as BsModalRef
  loteAtual: Lote = {} as Lote;


  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private activatedRouter: ActivatedRoute,
              private eventoService: EventoService,
              private loteService: LoteService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private router: Router,
              private modalService:BsModalService) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
    this.form.get('telefone')?.valueChanges.subscribe(() => {
      if (this.form.get('telefone')?.value[2] === "9") {
        this.telMask = '(00) 0 0000-0000'
      } else {
         this.telMask = '(00) 0000-0000'
      }
    });
  }

  public carregarEvento(): void {
    this.eventoId = parseInt(this.activatedRouter.snapshot.paramMap.get('id') || '0');
    if (this.eventoId !== null || this.eventoId === 0) {
      this.spinner.show().then();

      if (this.eventoId > 0) {
      this.modoSalvar = 'put';
      }

      this.eventoService.getEventoById(this.eventoId).subscribe({
        next: (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
          this.evento.lotes.forEach((lote: Lote) => {
          this.lotes.push(this.criarLote(lote));

          //this.carregarLotes();

          });
        },
        error: (error: Error) => {
          this.spinner.hide().then();
          this.toastr.error('Erro ao tentar carregar evento.', 'Erro!');
          console.error(error);
        }
      }).add(()=> this.spinner.hide());
    }
  }

  // public carregarLotes(): void {
  //   this.loteService.getLotesByEventoId(this.eventoId).subscribe({
  //     next: (lotesRetorno: Lote[]) => {
  //       lotesRetorno.forEach((lote: Lote) => {
  //         this.lotes.push(this.criarLote(lote));
  //       });
  //     },
  //     error: (error: Error) => {
  //       this.toastr.error('Erro ao tentar carregar lotes.', 'Erro!');
  //       console.error(error);
  //     }
  //   }).add(()=> this.spinner.hide());
  // }

  private validation(): void {
    this.form = this.fb.group({
      tema: ['',
              [
                Validators.required,
                Validators.minLength(this.minTemaLength),
                Validators.maxLength(this.maxTemaLength)
              ]
            ],
      local: ['', Validators.required],
      dataEvento: ['',
                    [
                      Validators.required
                    ]
                  ],
      qtdPessoas: ['',
                    [
                      Validators.required,
                      Validators.max(this.maxQtdPessoas)
                    ]
                  ],
      telefone: ['', Validators.required],
      email: ['',
                [
                  Validators.required,
                  Validators.email
                ]
              ],
      imagemURL: ['', Validators.required],
      lotes: this.fb.array([]),
    });
  }

  public adicionarLote(): void {
    this.lotes.push(this.criarLote({id: 0} as Lote));
  }

  private criarLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicio: [lote.dataInicio, Validators.required],
      dataFim: [lote.dataFim, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
    });
  }

  public get controls(): { [key: string]: AbstractControl }  {
    return this.form.controls;
  }

  public get bsConfig(): object {
    return { isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY HH:mm',
      showWeekNumbers: false,
      containerClass:'theme-dark-blue',
      };
  }

  public get bsDateConfig(): object {
    return { isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      showWeekNumbers: false,
      containerClass:'theme-dark-blue',
    };
  }

  public get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

  public get modoEditar(): boolean {
    return this.modoSalvar === 'put';
  }


  public resetForm(event: Event): void {
    event.preventDefault();
    this.form.reset();
  }

  public cssValidator(campoForm: AbstractControl | null): { 'is-invalid': boolean | null } {

    return campoForm === null
      ? { 'is-invalid': null }
      : { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarEvento(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.evento = (this.modoSalvar === 'put'
        ? { id: this.evento.id, ...this.form.value }
        : this.form.value)    ;

    if (this.modoSalvar === 'put' ) {
      this.salvarAlteracao();
    } else {
      this.salvarNovoEvento();
    }
  }

  public salvarAlteracao(): void {
    this.spinner.show().then()
    if (this.form.valid) {
      this.eventoService.put(this.evento).subscribe({
        next: () => {
          this.toastr.success('Evento editado com sucesso!', 'Sucesso!');
        },
        error: (error: Error) => {
          console.error(error);
          this.toastr.error('Erro ao tentar editar evento.' + error.message, 'Erro!');
        }
      }).add(()=> this.spinner.hide());
    }
  }

  public salvarNovoEvento(): void {
    this.spinner.show().then();
    if (this.form.valid) {
      this.evento = { ...this.form.value };
      this.eventoService.post(this.evento).subscribe({
        next: (eventoRetornado: Evento) => {
          this.toastr.success('Evento salvo com sucesso!', 'Sucesso!');
          this.router.navigate([`eventos/detalhe/${eventoRetornado.id}`]).then();
        },
        error: (error: Error) => {
          console.error(error);
          this.toastr.error('Erro ao tentar salvar evento.', 'Erro!');
        }
      }).add(()=> this.spinner.hide());
    }
  }

  public salvarLotes(): void {
    if (this.form.controls['lotes'].invalid) {
      this.form.markAllAsTouched();
      this.toastr.error('Preencha todos os campos do lote.', 'Erro!');
      return;
    }
    this.saveLoteOnRemote(this.form.value.lotes);
  }

  private saveLoteOnRemote(lotes: Lote[]): void {
    this.spinner.show().then();
    this.loteService.saveLote(this.eventoId, lotes).subscribe({
      next: () => {
        this.toastr.success('Lote salvo com sucesso!', 'Sucesso!');
        this.lotes.reset();
      },
      error: (error: Error) => {
        console.error(error);
        this.toastr.error('Erro ao tentar salvar lote.', 'Erro!');
      }
    }).add(()=> this.spinner.hide());
  }

  public removerLote(i: number, template: TemplateRef<any>) {
    this.loteAtual = this.lotes.controls[i].value;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirmDeleteLote() {
    this.modalRef.hide();
    this.spinner.show().then();

    this.loteService.deleteLote(this.eventoId, this.loteAtual.id).subscribe({
      next: () => {
        this.toastr.success('Lote removido com sucesso!', 'Sucesso!');
        this.lotes.removeAt(this.loteAtual.id)
      },
      error: (error: Error) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar remover lote ${this.loteAtual.id}.`, 'Erro!');
      }
    }).add(()=> this.spinner.hide());
    }

  public declineDeleteLote() {
    this.modalRef.hide();
  }

}
