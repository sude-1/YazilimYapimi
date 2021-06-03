import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators} from "@angular/forms"
import { ReportService } from 'src/app/services/report.service';
import { UserService } from 'src/app/services/user.service';
@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {

  reportForm:FormGroup
  constructor(private formBuilder:FormBuilder, private reportService:ReportService, private userService:UserService) { }
  startDate:Date
  ngOnInit(): void {
    this.createReportForm();
  }

  createReportForm(){
    this.reportForm= this.formBuilder.group({
      userId:this.userService.getUserId(),
      startDate:["",Validators.required],
      endDate:["",Validators.required]
    })
  }

  create(){
    if(this.reportForm.valid){
      let reportModel= Object.assign({},this.reportForm.value)
      this.reportService.report(reportModel).subscribe(response=>{
       window.location.href="https://localhost:44346/csv/"+this.userService.getUserId()+".csv"
      })
    }
  }
}
