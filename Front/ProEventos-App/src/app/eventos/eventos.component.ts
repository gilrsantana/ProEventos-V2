import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    // this.http.get('http://127.0.0.1:5207/Evento/Get')
    // .subscribe(response => this.eventos = response,
    //     error => console.log(error)
    //   );

    this.http.get('http://127.0.0.1:5207/Evento/Get').subscribe({
      next: (response) => {
        this.eventos = response;
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
        console.log('Finalizado');
      },
    });
  }
}
