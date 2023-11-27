import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
  name: 'datePipeBr',
})
export class DatePipeBrPipe implements PipeTransform {
  transform(value: string | Date | undefined): string | null {
    if (!value) {
      return null;
    }

    if (typeof value === 'string') {
      const [date] = value.split(' ');
      const [day, month, year] = date.split('/');
      const dateObj = new Date(+year, +month - 1, +day);

      const datePipe = new DatePipe('pt-BR');
      return datePipe.transform(dateObj, 'dd/MM/yyyy');
    } 

    if (value instanceof Date) {
      const datePipe = new DatePipe('pt-BR');
      return datePipe.transform(value, 'dd/MM/yyyy');
    }

    return '';
  }
}
