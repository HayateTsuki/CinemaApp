import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  public isTokenExpired(token: string): boolean {
    const tokenExpirationDate = this.getTokenExpirationDate(token);

    if (tokenExpirationDate == null) {
        return true;
    }

    return tokenExpirationDate.valueOf() < new Date().valueOf();
  }

  private getTokenExpirationDate(token: string): Date | null {
    let decoded: any;
    decoded = this.decodeToken(token);

    if (!decoded.hasOwnProperty('exp')) {
        return null;
    }

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  private decodeToken(token: string): any {
    const parts = token.split('.');

    if (parts.length !== 3) {
        throw new Error('JWT must have 3 parts');
    }

    const decoded = this.urlBase64Decode(parts[1]);
    if (!decoded) {
        throw new Error('Cannot decode the token');
    }

    return JSON.parse(decoded);
  }

  private urlBase64Decode(str: string): string {
    let output = str.replace(/-/g, '+').replace(/_/g, '/');
    switch (output.length % 4) {
        case 0: { break; }
        case 2: { output += '=='; break; }
        case 3: { output += '='; break; }
        default: {
            throw new Error('Illegal base64url string!');
        }
    }
    return this.b64DecodeUnicode(output);
  }

  private b64DecodeUnicode(str: any) {
    return decodeURIComponent(Array.prototype.map.call(atob(str), (c: any) => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
  }
}
