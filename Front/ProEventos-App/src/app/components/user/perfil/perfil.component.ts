import { Component, OnInit } from '@angular/core';
import { AbstractControl, AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/validator-field';
import { Constants } from '@app/util/constants';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit{

  form!: FormGroup;
  telefone = '';
  telMask= '(00) 0000-00000'
  constants = Constants;
  errorMessenger = ValidatorField.getErrorMessage;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();

    this.form.get('phone')?.valueChanges.subscribe(() => {
      if (this.form.get('phone')?.value[2] === "9") {
        this.telMask = '(00) 0 0000-0000'
      } else {
         this.telMask = '(00) 0000-0000'
      }
    });
  }

  private validation(): void {

    const formOptions: AbstractControlOptions = { 
      validators: ValidatorField.MustMatch('password', 'confirmPassword')
    };

    this.form = this.fb.group({
      title: [''],
      firstName: ['',
        [
          Validators.required,
          Validators.minLength(this.constants.MIN_NAME_LENGTH), 
          Validators.maxLength(this.constants.MAX_NAME_LENGTH),
          Validators.pattern(/^[a-zA-Z]+$/)
        ]
      ],
      lastName: ['',
        [
          Validators.required,
          Validators.minLength(this.constants.MIN_NAME_LENGTH), 
          Validators.maxLength(this.constants.MAX_NAME_LENGTH),
          Validators.pattern(/^[a-zA-Z]+$/)
        ]
      ],
      email: ['',
        [
          Validators.required,
          Validators.email
        ]
      ],
      phone: ['',
        [
          Validators.required,
          Validators.minLength(this.constants.MIN_PHONE_LENGTH)
        ]
      ],
      description: [''],
      password: ['',
        [ Validators.pattern(this.constants.PASSWORD_PATTERN)]
      ],
      confirmPassword: ['',
        []
      ]
    }, formOptions);
  }

  public get controls(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  public resetForm(event: Event): void {
    event.preventDefault();
    this.form.reset();
  }

  public onSubmit(): void {

    this.validateForm();
    if (this.form.valid) {
      console.log('Enviado');
    }
  }

  public validateForm(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
  }

  public cssValidator(campoForm: AbstractControl): { 'is-invalid': boolean | null } {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }
}
