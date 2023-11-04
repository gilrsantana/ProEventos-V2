import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})


export class EventoDetalheComponent implements OnInit{
  
  form!: FormGroup;
  
  minTemaLength = 3;
  maxTemaLength = 50;
  minDataEvento = new Date().getDate();
  maxQtdPessoas = 120000;


  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
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

  public get controls(): any {
    return this.form.controls;
  }

  public resetForm(): void {
    this.form.reset();
  }
}