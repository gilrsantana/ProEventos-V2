import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})


export class EventoDetalheComponent implements OnInit{
  
  form!: FormGroup;
  evento = {} as Evento;
  minTemaLength = 3;
  maxTemaLength = 50;
  minDataEvento = new Date().getDate() + 1;
  maxQtdPessoas = 120000;

  constructor(private fb: FormBuilder, 
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private eventoService: EventoService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService) { 
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public carregarEvento(): void {
    const eventoIdParam = this.router.snapshot.paramMap.get('id');
    if (eventoIdParam !== null) {
      this.spinner.show();
      this.eventoService.getEventoById(+eventoIdParam).subscribe({
        next: (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
        },
        error: (error: Error) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar evento.', 'Erro!');
          console.error(error);
        },
        complete: () => {
          this.spinner.hide();
        }
      });
    }
  }

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
                      Validators.required, 
                      Validators.min(this.minDataEvento)
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

  public resetForm(event: Event): void {
    event.preventDefault();
    this.form.reset();
  }

  public cssValidator(campoForm: AbstractControl): { 'is-invalid': boolean | null } {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }
}
