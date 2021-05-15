import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';
import { Wallet } from 'src/app/models/wallet';
import { UserService } from 'src/app/services/user.service';
import { ProductService } from 'src/app/services/product.service';
import { WalletService } from 'src/app/services/wallet.service';
import { AuthService } from 'src/app/services/auth.service';
import { AdminService } from 'src/app/services/admin.service';
import { Router } from '@angular/router'
@Component({
  selector: 'app-navi',
  templateUrl: './navi.component.html',
  styleUrls: ['./navi.component.css']
})
export class NaviComponent implements OnInit {

  loginsuccesful:boolean;
  admin:boolean;
  user:number;
  wallet:number;
  productToBeApprove:Product[];
  moneyToBeApprove:Wallet[];
  toBeApprove:boolean=false
  constructor(private walletService:WalletService, private productService:ProductService, 
    private userService:UserService, private authService:AuthService, private adminService:AdminService,private router:Router) { }

  ngOnInit(): void {
    this.loginsuccesful = this.authService.isAuthenticated();
    if(this.loginsuccesful){
      this.admin = this.userService.roleControl(['admin'])//giriş yapıldıysa admin mi
      this.user = this.userService.getUserId();
      if(this.admin){
        this.adminService.getAddMoney().subscribe(response=>{
          this.moneyToBeApprove = response.data;
          if(response.data.length>0)
          this.toBeApprove = true;
        },errorResponse=>{
          console.log(errorResponse);
        });

        this.adminService.getAddProduct().subscribe(response=>{
          this.productToBeApprove = response.data;
          if(response.data.length>0){
            this.toBeApprove=true;
          }
        },errorResponse=>{
          console.log(errorResponse);
        })
      }
    }
  }

  approveProduct(product:Product){
    console.log(product)
    this.adminService.approveAddProduct(product).subscribe(response=>{
      this.productToBeApprove = this.productToBeApprove.filter(p=>p!=product)
    },errorRepsonse=>{
      console.log(errorRepsonse);
    })
  }

  approveMoney(wallet:Wallet){
    this.adminService.approveAddMoney(wallet).subscribe(response=>{
      this.moneyToBeApprove = this.moneyToBeApprove.filter(w=>w!=wallet)
    },errorResponse=>{
      console.log(errorResponse);
    })
  }

  refusalProduct(product:Product){
    this.adminService.refusalAddProduct(product).subscribe(response=>{
      this.productToBeApprove = this.productToBeApprove.filter(p=>p != product)
    },errorResponse=>{
      console.log(errorResponse);
    })
  }

  refusalMoney(wallet : Wallet){
    this.adminService.refusalAddMoney(wallet).subscribe(response=>{
      this.moneyToBeApprove = this.moneyToBeApprove.filter(w=>w != wallet)
    },errorResponse=>{
      console.log(errorResponse);
    })
  }

  login(){
    this.router.navigate(["login"]);
  }
  logOut(){
    localStorage.removeItem("token")
    window.location.reload();
  }
  register(){
    this.router.navigate(["register"]);
  }
}
