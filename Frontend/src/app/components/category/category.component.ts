import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories : Category[]=[];
  currentCategory : Category={categoryId:-1,categoryName:""} ;
  
  constructor(private categoryService:CategoryService,private activatedRoute:ActivatedRoute) { }
  
  ngOnInit(): void { 
    this.getCategories();
    this.activatedRoute.params.subscribe(params=>{
    if(params["categoryId"]){
      this.currentCategory.categoryId=params["categoryId"]
    }
  })
  }

  getCategories(){
    this.categoryService.getCategories().subscribe(response=>{
      this.categories = response.data
    })
  }

  setCurrentCategory(category:Category){
    this.currentCategory = category;
  }

  getCurrentCategoryClass(category:Category){
    if(category.categoryId ==this.currentCategory.categoryId){
      return "list-group-item active btn"
    }
    else
    {
      return "list-group-item btn"
    }
  }

  getAllCategoryClass(){
    if(this.currentCategory.categoryId==-1){
      return "list-group-item active btn"
    }
    else
    {
      return "list-group-item btn"
    }
  }
  
   deleteCurrentCategory(){
     this.currentCategory.categoryId = -1;
   }

}
