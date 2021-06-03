import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Currency } from '../models/currency';
import { ListResponseModel } from '../models/listResponseModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  apiUrl = "https://localhost:44346/api/currencies/getall";  

  constructor(private httpClient:HttpClient) { }

  getall():Observable<ListResponseModel<Currency>>{
   return this.httpClient.get<ListResponseModel<Currency>>(this.apiUrl)
  }
}