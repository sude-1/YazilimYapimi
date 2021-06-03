import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators} from "@angular/forms"
import { ToastrService } from 'ngx-toastr';
import { Currency } from 'src/app/models/currency';
import { Wallet } from 'src/app/models/wallet';
import { CurrencyService } from 'src/app/services/currency.service';
import { UserService } from 'src/app/services/user.service';
import { WalletService } from 'src/app/services/wallet.service';

@Component({
  selector: 'app-money-add',
  templateUrl: './money-add.component.html',
  styleUrls: ['./money-add.component.css']
})
export class MoneyAddComponent implements OnInit {

  moneyAddForm : FormGroup;
  currencies : Currency[]; 
  constructor(private formBuilder:FormBuilder, private walletService:WalletService, 
    private toastrService:ToastrService,private userService:UserService, private currencyService:CurrencyService) { }

  ngOnInit(): void {
    this.createMoneyAddForm();
    this.currencyService.getall().subscribe(response=>{
      this.currencies=response.data
    })
  }

  createMoneyAddForm(){
    this.moneyAddForm = this.formBuilder.group({
      userId:this.userService.getUserId(),
      money:["",Validators.required],
      currencyId:['',Validators.required]
    })
  }
  add(){
    if(this.moneyAddForm.valid){
      let moneyModel:Wallet = Object.assign({},this.moneyAddForm.value)//içi boş bir obje oluşturuyor moneyModel için virgülden sonraki alanları eklicek
      moneyModel.currencyId=Number(moneyModel.currencyId)
      this.walletService.add(moneyModel).subscribe(response=>{
        this.toastrService.success(response.message,"Başarılı")
      },responseError=>{
        if(responseError.error.ValidationErrors.length>0){
          for (let i = 0; i < responseError.error.ValidationErrors.length; i++) {
            this.toastrService.error(responseError.error.ValidationErrors[i].ErrorMessage, 'Hata');
          }
        }
        
      })  
    }else{
      this.toastrService.error("Formu Kontrol Ediniz")
    }    
  }

}
