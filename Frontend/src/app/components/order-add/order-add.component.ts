import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators} from "@angular/forms"

@Component({
  selector: 'app-order-add',
  templateUrl: './order-add.component.html',
  styleUrls: ['./order-add.component.css']
})
export class OrderAddComponent implements OnInit {

  orderAddForm:FormGroup;
  constructor(private formBuilder:FormBuilder) { }

  ngOnInit(): void {
    this.createAddOrderForm();
  }

  createAddOrderForm(){
    this.orderAddForm = this.formBuilder.group({
      productName:["",Validators.required],
      userName:["",Validators.required],
      orderDate:["",Validators.required],
      quantity:["",Validators.required],
      total:["",Validators.required]
    })
    
  }
}
