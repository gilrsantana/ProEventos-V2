import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants as AppConstants } from '../util/constants';

@Pipe({
  name: 'dateTimeFormat'
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {

  override transform(value: any, args?: any): any {
    return super.transform(value, AppConstants.DATE_TIME_FORMAT_BR)
  }
  // transform(value: Date | undefined): string {
  //   const datePipe: DatePipe = new DatePipe('pt-BR');
  //   console.log('value: ', value)
  //   const result = datePipe.transform(value, AppConstants.DATE_TIME_FORMAT_BR) || '';
  //   return result;
  // }
}
