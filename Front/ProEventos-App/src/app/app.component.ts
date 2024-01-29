import {Component, OnInit} from '@angular/core';
import {AccountService} from "@app/services/account.service";
import {User} from "@app/models/Identity/user";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'ProEventos-App';
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(): void {
    const key = this.accountService.storageKey;
    let user: User = {} as User;
    if (localStorage.getItem(key)) {
      user = JSON.parse(localStorage.getItem('user')  ?? '{}') as User;
    }
    this.accountService.setCurrentUser(user);
  }
}
