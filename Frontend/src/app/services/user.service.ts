import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private authService: AuthService) {}
  isUserHaveClaims(necessaryClaims: string[]): boolean {
    //verilen role/rollere sahip mi
    if (necessaryClaims == undefined) return false;
    if (this.authService.isAuthenticated() == false) {
      return false;
    }
    let isUserHaveClaim: boolean = false;

    let claims: string = JSON.parse(
      JSON.stringify(jwt_decode(localStorage.getItem('token')))
    )['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']; //jwt üzerinden decode ile çekiyor
    claims.split(',').forEach((ownedClaim) => {
      //string olarak gelen rolleri virgül ile ayırıp rol karşılaştırıyor
      necessaryClaims.forEach((necessaryClaim) => {
        if (ownedClaim === 'admin' || ownedClaim === necessaryClaim) {
          isUserHaveClaim = true;
        }
      });
    });
    return isUserHaveClaim;
  }
  getUserId(): number {
    if (this.authService.isAuthenticated()) {
      return Number(
        JSON.parse(JSON.stringify(jwt_decode(localStorage.getItem('token'))))[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ]
      );
    }
    return null;
  }

}
