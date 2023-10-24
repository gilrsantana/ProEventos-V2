import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants as AppConstants } from '../util/constants';

@Pipe({
  name: 'dateTimeFormat'
})
export class DateTimeFormatPipe implements PipeTransform {

  transform(value: Date | undefined): string {
    const datePipe: DatePipe = new DatePipe('en-US');
    const result = datePipe.transform(value, AppConstants.DATE_TIME_FORMAT_BR) || '';
    return result;
  }

}
