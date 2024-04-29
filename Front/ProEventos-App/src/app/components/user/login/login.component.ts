import {Component} from '@angular/core';
import {UserLogin} from "@app/models/Identity/user-login";
import {AccountService} from "@app/services/account.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent{
  model = {} as UserLogin;
  constructor(private accountService: AccountService,
              private router: Router,
              private toastr: ToastrService) { }

  public login(): void {
    this.accountService.login(this.model).subscribe(
      () => {
        this.router.navigateByUrl('/dashboard');
        this.toastr.success('Login realizado com sucesso!');
      },
      (error) => {
        if(error.status === 401)
          this.toastr.error('Usuário e/ou senha incorretos');
        else {
          console.error(error);
          this.toastr.error('Falha ao tentar realizar login');
        }
      }
    );
  }
}
