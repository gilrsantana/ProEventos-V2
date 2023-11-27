import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateTimePipeBr'
})
export class DateTimePipeBrPipe implements PipeTransform {

  transform(value: string | undefined): string | null {
    if (!value) {
      return null;
    }

    const [date, time] = value.split(' ');
    const [day, month, year] = date.split('/');
    const [hour, minute, second] = time.split(':');
    const dateObj = new Date(+year, +month - 1, +day, +hour, +minute, +second);

    const datePipe = new DatePipe('pt-BR');
    return datePipe.transform(dateObj, 'dd/MM/yyyy HH:mm');
  }

}
