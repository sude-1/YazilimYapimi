import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators} from "@angular/forms"
import { ToastrService } from 'ngx-toastr';
import { Wallet } from 'src/app/models/wallet';
import { UserService } from 'src/app/services/user.service';
import { WalletService } from 'src/app/services/wallet.service';

@Component({
  selector: 'app-money-add',
  templateUrl: './money-add.component.html',
  styleUrls: ['./money-add.component.css']
})
export class MoneyAddComponent implements OnInit {

  moneyAddForm : FormGroup;
  constructor(private formBuilder:FormBuilder, private walletService:WalletService, private toastrService:ToastrService,private userService:UserService ) { }

  ngOnInit(): void {
    this.createMoneyAddForm();
  }

  createMoneyAddForm(){
    this.moneyAddForm = this.formBuilder.group({
      userId:this.userService.getUserId(),
      money:["",Validators.required]
    })
  }
  add(){
    if(this.moneyAddForm.valid){
      let moneyModel:Wallet = Object.assign({},this.moneyAddForm.value)//içi boş bir obje oluşturuyor moneyModel için virgülden sonraki alanları eklicek
      this.walletService.add(moneyModel).subscribe(response=>{
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
