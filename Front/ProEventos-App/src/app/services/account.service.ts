import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "@environments/environment";
import {Observable, ReplaySubject} from "rxjs";
import { map, take } from 'rxjs/operators';
import {User} from "@app/models/Identity/user";
import {UserLogin} from "@app/models/Identity/user-login";
import {SecureTokenService} from "@app/services/secure-token.service";

@Injectable()
export class AccountService {
  private currentUserSource = new ReplaySubject<string|null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  baseUrl = environment.apiUrl + 'api/account/';
  storageKey = '__app_data';
  constructor(private httpCliente: HttpClient,
              private secureTokenService: SecureTokenService) { }

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
    localStorage.removeItem(this.storageKey);
    this.currentUserSource.next(null);
    this.currentUserSource.complete();
  }

  public isLoggedIn(): boolean {
    const encrypted = localStorage.getItem(this.storageKey);
    return !!encrypted;
  }

  public getUserName(): string {
    const user = localStorage.getItem(this.storageKey);
    if(user) {
      return  JSON.parse(
        this.secureTokenService.decryptData(user))
        .username;
    }
    return '';
  }

  public getCurrentUser(): User|null {
    const encrypted = localStorage.getItem(this.storageKey);
    if(encrypted) {
      return JSON.parse(
        this.secureTokenService.decryptData(encrypted));
    }
    return null;
  }

  public setCurrentUser(user: User): void {
    const encrypted = this.secureTokenService.encryptData(JSON.stringify(user));
    console.log(encrypted);
    localStorage.setItem(this.storageKey, encrypted);
    this.currentUserSource.next(encrypted);
  }

  public isTokenExpired(): boolean {
    const user = this.getCurrentUser();
    if(user) {
      return this.secureTokenService.isTokenExpired(user.token);
    }
    return false;
  }
}
