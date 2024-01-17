import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "@environments/environment";
import {Observable, ReplaySubject} from "rxjs";
import { map, take } from 'rxjs/operators';
import {User} from "@app/models/Identity/user";
import {UserLogin} from "@app/models/Identity/user-login";

@Injectable()
export class AccountService {
  private currentUserSource = new ReplaySubject<User|null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  baseUrl = environment.apiUrl + 'api/account/';
  constructor(private httpCliente: HttpClient) { }

  public login(model: UserLogin): Observable<void> {
    return this.httpCliente.post<User>(this.baseUrl + 'login', model)
      .pipe(
        take(1),
        map((response : User) => {
          const user = response;
          if(user) {
            this.setCurrentUser(user);
          }
        })
      );
  }

  public register(model: any): Observable<void> {
    return this.httpCliente.post<User>(this.baseUrl + 'register', model)
      .pipe(
        take(1),
        map((response : User) => {
          const user = response;
          if(user) {
            this.setCurrentUser(user);
          }
        })
      );
  }

  public logout(): void {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentUserSource.complete();
  }

  public isLoggedIn(): boolean {
    const user = localStorage.getItem('user');

    return !!user;
  }

  public getUserName(): string {
    const user = localStorage.getItem('user');
    if(user) {
      return JSON.parse(user).username;
    }
    return '';
  }

  public setCurrentUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
}
