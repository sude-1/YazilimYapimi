import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators} from "@angular/forms"

@Component({
  selector: 'app-money-add',
  templateUrl: './money-add.component.html',
  styleUrls: ['./money-add.component.css']
})
export class MoneyAddComponent implements OnInit {

  moneyAddForm : FormGroup;
  constructor(private formBuilder:FormBuilder) { }

  ngOnInit(): void {
    this.createMoneyAddForm();
  }

  createMoneyAddForm(){
    this.moneyAddForm = this.formBuilder.group({
      userId:["",Validators.required],
      money:["",Validators.required]
    })
  }
}
