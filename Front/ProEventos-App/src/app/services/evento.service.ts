import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable(
  /*
   * A injeção de dependência pode ser feita de três formas:
   * 1- No componente de serviço declarando que ele é injetado na raiz do projeto
   * 2- No componente que irá utilizar o serviço
   * 3- No app.module.ts, que tem as configurações gerais
   * Por padrão, vamos adotar a opção 3 neste projeto
   * A linha abaixo, comentada, é a forma 1
   * { providedIn: 'root' }
  */
)
export class EventoService {
  baseURL = 'http://127.0.0.1:5207/Evento'

  constructor(private http: HttpClient) { }

  getEventos(): Observable<Evento[]> {
    return this.http
      .get<Evento[]>(`${this.baseURL}/Get`)
      .pipe(take(1));
  }

  getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http
      .get<Evento[]>(`${this.baseURL}/GetByTema/${tema}`)
      .pipe(take(1));
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http
      .get<Evento>(`${this.baseURL}/GetById/${id}`)
      .pipe(take(1));
  }
  
  post(evento: Evento): Observable<Evento> {
    return this.http
      .post<Evento>(`${this.baseURL}/Post`, evento)
      .pipe(take(1));
  }

  put(evento: Evento): Observable<Evento> {
    return this.http
      .put<Evento>(`${this.baseURL}/Put/${evento.id}`, evento)
      .pipe(take(1));
  }

  deleteEvento(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/Delete/${id}`)
      .pipe(take(1));
  }
}