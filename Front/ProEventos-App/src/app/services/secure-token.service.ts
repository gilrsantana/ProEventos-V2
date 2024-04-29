import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import {jwtDecode} from 'jwt-decode';
import {environment} from "@environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SecureTokenService {

  encryptData(data: string): string {
    return CryptoJS.AES.encrypt(data, environment.secureKey).toString();
  }

  decryptData(encryptedData: string): string {
    const decrypted = CryptoJS.AES.decrypt(encryptedData, environment.secureKey);
    return decrypted.toString(CryptoJS.enc.Utf8);
  }

  private getExpirationDate(token: string): Date | null {
    const decodedToken: any = jwtDecode(token);

    if (decodedToken.exp === undefined) {
      return null;
    }

    const expirationDate = new Date(0);
    expirationDate.setUTCSeconds(decodedToken.exp);

    return expirationDate;
  }

  isTokenExpired(token: string): boolean {
    const expirationDate = this.getExpirationDate(token);
    if (expirationDate === null) {
      return false;
    }
  expirationDate.setMinutes(expirationDate.getMinutes() - 10);
    return expirationDate.valueOf() <= new Date().valueOf();
  }

}
