import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent {
  @Input() titulo:string | undefined;
  @Input() subtitulo = 'Desde 2023';
  @Input() iconClass = 'fa fa-user';
  @Input() botaoListar = false;
}
