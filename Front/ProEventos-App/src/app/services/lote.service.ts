import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { environment } from '@environments/environment';
import { take } from 'rxjs';

@Injectable()
export class LoteService {
  baseURL = `${environment.apiUrl}Lote`;
  
  constructor(private http: HttpClient) { }

  getLotesByEventoId(eventoId: number) {
    return this.http
    .get<Lote[]>(`${this.baseURL}/Get/${eventoId}`)
    .pipe(take(1));
  }

  saveLote(eventoId: number, lotes: Lote[]) {
    return this.http
    .put<Lote[]>(`${this.baseURL}/Put/${eventoId}`, lotes)
    .pipe(take(1));
  }

  deleteLote(eventoId: number, loteId: number) {
    return this.http
    .delete(`${this.baseURL}/Delete/${eventoId}/${loteId}`)
    .pipe(take(1));
  }

}
