import { Injectable } from '@angular/core';
import { Wallet } from '../models/wallet';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SingleResponseModel } from '../models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class WalletService {

  apiUrl = "https://localhost:44346/api/userswallets";  

  constructor(private httpClient:HttpClient) { }

  add(wallet:Wallet):Observable<SingleResponseModel<Wallet>>{
    return this.httpClient.post<SingleResponseModel<Wallet>>(this.apiUrl+"/add",wallet)
  }
  getByUserId(id:number):Observable<SingleResponseModel<Wallet>>{
    return this.httpClient.get<SingleResponseModel<Wallet>>(this.apiUrl+"/getbyid?userId="+id)
  }
}
