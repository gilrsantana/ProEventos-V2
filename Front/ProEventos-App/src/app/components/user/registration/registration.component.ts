import { Component, OnInit } from '@angular/core';
import { AbstractControl, AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/validator-field';
import { Constants } from '@app/util/constants';
import {User} from "@app/models/Identity/user";
import {AccountService} from "@app/services/account.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit{

  form!: FormGroup;
  constants = Constants;
  errorMessenger = ValidatorField.getErrorMessage;
  user = {} as User;

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmPassword')
    };

    this.form = this.fb.group({
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
      userName: ['',
        [
          Validators.required,
          Validators.minLength(this.constants.MIN_USER_LENGTH),
          Validators.maxLength(this.constants.MAX_USER_LENGTH),
          Validators.pattern(/^[a-zA-Z0-9][0-9a-zA-Z!@#$%^&*()_+={}[\]|\\:;"'<>,.?/]+$/)
        ]
      ],
      password: ['',
        [
          Validators.required,
          Validators.pattern(this.constants.PASSWORD_PATTERN)
        ]
      ],
      confirmPassword: ['',
        [Validators.required]
      ],
      terms: [false, Validators.requiredTrue]
    }, formOptions);
  }

  public get controls(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  public validateForm(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
  }

  register(): void {
    this.user = { ... this.form.value };
    this.accountService.register(this.user).subscribe(
      () => {
        this.router.navigateByUrl('/dashboard');
        this.toastr.success('Cadastro realizado com sucesso!');
      },
      (error) => {
        console.error(error);
        this.toastr.error('Erro ao realizar cadastro!');
      }
    );
  }

}
