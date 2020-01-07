import { Component, OnInit } from '@angular/core';
import { Product } from '../product';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ProductsService } from './products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
 public products:Product[]=[];
 public openFilters:boolean=false
 public filterForm:FormGroup;
 public categories:string[]=[];
 public subcategories:string[]=[];
 public page:number=1;
 public size:number=30;
 public isCategorySet:boolean=false;
 public grid:boolean=true;
  constructor(private formBuilder:FormBuilder,private productsService:ProductsService) {

  }

  ngOnInit() {
    this.filterForm= this.formBuilder.group({
    category:[''],
    subcategory:[''],
    price:['']

});
 this.getProducts();
// this.productsService.getCategories().subscribe(categories=>{this.categories=categories
// console.log(categories)})

  }

public setSubcategories(id:string){
  console.log(id);
  this.productsService.getSubcategories(id).subscribe(sub=>{this.subcategories=sub;
    console.log(sub)});
    this.isCategorySet=true;
}
public getProducts(){
  this.productsService.getProducts(30,0,"","",0,100).subscribe(products=>{this.products=products;
    console.log(products)})
}
public filterProducts(){
  this.productsService.getProducts(30,0,this.filterForm.value.category,this.filterForm.value.subcategory,0,this.filterForm.value.price).subscribe(prod=>
   { this.products=prod
  console.log(prod)});
}
public clearFilters(){
  this.filterForm.controls['category'].setValue('');
  this.filterForm.controls['subcategory'].setValue('');
  this.filterForm.controls['price'].setValue('');
  this.getProducts();
}
public switchView(view:string){
if(view=="list"){
  this.grid=false;
}
else{
  this.grid=true;
}
}
}
