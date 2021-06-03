import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SingleResponseModel } from '../models/singleResponseModel';
import { Report } from '../models/report';


@Injectable({
  providedIn: 'root'
})
export class ReportService {

  apiUrl = "https://localhost:44346/api/";

  constructor(private httpClient:HttpClient) { }
  report(report:Report):Observable<SingleResponseModel<Report>>{
    return this.httpClient.post<SingleResponseModel<Report>>(this.apiUrl+"reports/report",report)
  }
}
