import { Component, OnInit } from '@angular/core';
import {FormGroup,FormBuilder,FormControl,Validators} from '@angular/forms'
import { ProductService } from 'src/app/services/product.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.css']
})
export class ProductAddComponent implements OnInit {

  productAddForm:FormGroup;
  constructor(private formBuilder:FormBuilder, private productService:ProductService, private toastrService:ToastrService,
    private userService:UserService) { }

  ngOnInit(): void {
 
    this.createProductAddForm(); 
  }

  createProductAddForm(){
    this.productAddForm=this.formBuilder.group({
      productName:["",Validators.required],
      unitPrice:["",Validators.required],
      quantity:["",Validators.required],
      categoryId:["",Validators.required],
      supplierId:this.userService.getUserId()
    })
  }

  add(){
    if(this.productAddForm.valid){
      let productmodel = Object.assign({},this.productAddForm.value) 
      this.productService.add(productmodel).subscribe(response=>{
        this.toastrService.success(response.message,"Ürün başarıyla eklendi")
      },responseError=>{
        console.log(responseError)
        this.toastrService.error(responseError.error.message)
      })
      
    }else{
      this.toastrService.error("Formunuz eksik")
    }
  }
}
