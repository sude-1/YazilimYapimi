import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { Product } from '../models/product';
import { ResponseModel } from '../models/responseModel';
import { Wallet } from '../models/wallet';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  apiUrl = "https://localhost:44346/api/admin";  

  constructor(private httpClient:HttpClient) { }
 
  getAddMoney():Observable<ListResponseModel<Wallet>>{
    return this.httpClient.get<ListResponseModel<Wallet>>(this.apiUrl+"/getapproveaddmoney")
  }

  getAddProduct():Observable<ListResponseModel<Product>>{
    return this.httpClient.get<ListResponseModel<Product>>(this.apiUrl+"/getapproveaddproduct")
  }

  approveAddProduct(product:Product):Observable<ResponseModel>{
    return this.httpClient.post<ResponseModel>(this.apiUrl+"/approveaddproduct",product)
  }

  refusalAddProduct(product:Product):Observable<ResponseModel>{
    return this.httpClient.post<ResponseModel>(this.apiUrl+"/refusaladdproduct",product)
  }

  approveAddMoney(money:Wallet):Observable<ResponseModel>{
    return this.httpClient.post<ResponseModel>(this.apiUrl+"/approveaddmoney",money)
  }
  
  refusalAddMoney(money:Wallet):Observable<ResponseModel>{
    return this.httpClient.post<ResponseModel>(this.apiUrl+"/refusaladdmoney",money)
  }
}
