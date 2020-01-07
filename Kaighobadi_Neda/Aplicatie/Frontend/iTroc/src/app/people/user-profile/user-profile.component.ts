import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/user';
import { ActivatedRoute, Params } from '@angular/router';
import { PeopleService } from '../people.service';
import { UserProfile } from 'src/app/userProfile';
import { ProductsService } from 'src/app/products/products.service';
import { Product } from 'src/app/product';
import { LoginService } from 'src/app/login/login.service';
import { MatDialog } from '@angular/material';
import { AddProductComponent } from './add-product/add-product.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
public user:any;
public currentUser:any;
public userProfile: UserProfile;
public userId:string;
public products: Product;
  constructor(private activatedRoute:ActivatedRoute,private peopleService: PeopleService,
    private productsService: ProductsService,private loginService: LoginService,
    public dialog :MatDialog,public formBuilder: FormBuilder) { 
  }


  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.userId = params['id'];
      this.peopleService.getUser(this.userId).subscribe(user=>{this.user=user;console.log(user)});});
    if(this.userId){
      this.productsService.getProductsBySeller(this.userId).subscribe(products=> {this.products=products;
      console.log(this.products)});
    }
    this.currentUser=this.loginService.getUser();
  }

  public openAddProductDialog(product?:Product){
    let dialogref;
    if(product){
      let productForm:FormGroup;
       
        dialogref= this.dialog.open(AddProductComponent,{width:'50rem',data:{product:product}});
        dialogref.afterClosed().subscribe(el=> window.location.reload());

    }
    else{
      dialogref= this.dialog.open(AddProductComponent,{width:'50rem',data:{sellerId:this.user.Id,sellerAdress:this.user.adress.City+" "+this.user.adress.Country}});
      dialogref.afterClosed().subscribe(el=> window.location.reload());

    }
  }

}
