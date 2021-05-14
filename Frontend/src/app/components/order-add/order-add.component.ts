import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators} from "@angular/forms"
import { Order } from 'src/app/models/order';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-order-add',
  templateUrl: './order-add.component.html',
  styleUrls: ['./order-add.component.css']
})
export class OrderAddComponent implements OnInit {

  orderAddForm:FormGroup;
  categories: Category[];
  products:Product[];
  selectCategoryId:number
  constructor(private formBuilder:FormBuilder, private userService:UserService, 
    private orderService:OrderService, private toastrService:ToastrService, 
    private categoryService:CategoryService, private productService:ProductService) { }

  ngOnInit(): void {
    this.createAddOrderForm();
    
    this.categoryService.getCategories().subscribe(
      (response) => {
        this.categories = response.data;
      
        
      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );
  }

  createAddOrderForm(){
    this.orderAddForm = this.formBuilder.group({
      productName:["",Validators.required],
      customerId:this.userService.getUserId(),
      quantity:["",Validators.required],
      categoryId:["",Validators.required],
    })    
  }
  onChangeofOptions() {
    this.productService.getProductsByCategory(this.selectCategoryId).subscribe(
      (response) => {
        console.log(response)
        this.products = response.data;
      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );
}
  
  add(){
    if(this.orderAddForm.valid){
      let orderModel:Order = Object.assign({},this.orderAddForm.value)//içi boş bir obje oluşturuyor moneyModel için virgülden sonraki alanları eklicek
      orderModel.categoryId=Number(this.selectCategoryId)
      console.log(orderModel)
      this.orderService.add(orderModel).subscribe(response=>{
        this.toastrService.success(response.message,"Başarılı")
      },responseError=>{
        console.log(responseError)
        this.toastrService.error(responseError.error.message)
        
      })     
    }else{
      this.toastrService.error("Formu Kontrol Ediniz")
    }    
  }
}
