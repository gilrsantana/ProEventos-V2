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

    let [date, time] = value.split(' ');
    let [day, month, year] = date.split('/');
    let [hour, minute, second] = time.split(':');
    let dateObj = new Date(+year, +month - 1, +day, +hour, +minute, +second);

    let datePipe = new DatePipe('pt-BR');
    return datePipe.transform(dateObj, 'dd/MM/yyyy HH:mm');
  }

}
