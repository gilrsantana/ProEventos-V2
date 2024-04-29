import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {ToastrService} from "ngx-toastr";
import {AccountService} from "@app/services/account.service";

export const authGuard: CanActivateFn = () => {
  const key = inject(AccountService).storageKey;
  if (localStorage.getItem(key) !== null) {
    return true;
  }
  inject(ToastrService)
    .info('Você precisa estar logado para acessar essa página', 'Acesso Negado!')
  inject(Router)
    .navigate(['/user/login']).then();
  return false;
};
