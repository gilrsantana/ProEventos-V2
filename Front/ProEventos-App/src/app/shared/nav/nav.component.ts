import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {AccountService} from "@app/services/account.service";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent {
  isCollapsed = true;
  constructor(public accountService: AccountService,
              private router: Router) {  }

  get isLoggedIn(): boolean {
    return this.accountService.isLoggedIn();
  }
  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/user/login');
  }

  showMenu(): boolean {
    return this.router.url !== '/user/login';
  }
}
