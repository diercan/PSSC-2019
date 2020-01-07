import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Product } from 'src/app/product';
import { ProductsService } from 'src/app/products/products.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})
export class AddProductComponent implements OnInit {
  public productForm: FormGroup;
  private product: Product;
  private sellerId:string;
  private updateEnabled: boolean=false;
  private sellerAdress:string;
  constructor(public dialogRef: MatDialogRef<AddProductComponent>, private formBuilder: FormBuilder,private productService: ProductsService, @Inject(MAT_DIALOG_DATA) public data) { 
      this.sellerId=data.sellerId;
      this.sellerAdress=data.sellerAdress;
      if(data){
        this.product=new Product(data.product.pic,data.product.name,data.product.category,data.product.subcategory,data.product.description,data.product.price,data.product.sellerId,data.product.sellerAdress,data.product.id);
        console.log(this.product)
        this.updateEnabled=true;
       this.productForm = this.formBuilder.group({
          picUrl: [this.product.pic, Validators.required],
          name:[this.product.name,Validators.required],
          category: [this.product.category, Validators.required],
          subcategory:[this.product.subcategory,Validators.required],
          description: [this.product.description, Validators.required],
          price:[this.product.price,Validators.required]});
      
      }
      else{
        this.updateEnabled=false;
        this.productForm = this.formBuilder.group({
          picUrl: ['', Validators.required],
          name:['',Validators.required],
          category: ['', Validators.required],
          subcategory:['',Validators.required],
          description: ['', Validators.required],
          price:['',Validators.required]})
      }
      
}

  ngOnInit() {
  
  }
  
  public onClick(): void {
    
    if(this.updateEnabled){
      this.product.editProduct(this.productForm.value.picUrl,this.productForm.value.name,this.productForm.value.category,
        this.productForm.value.subcategory,this.productForm.value.description,this.productForm.value.price);
        this.productService.updateProduct(this.product).subscribe(product=> console.log(product));
        this.dialogRef.close();
    }
    else{
        this.product=new Product(this.productForm.value.picUrl,this.productForm.value.name,this.productForm.value.category,
        this.productForm.value.subcategory,this.productForm.value.description,this.productForm.value.price,this.sellerId,this.sellerAdress);
        this.productService.addProduct(this.product).subscribe(product=>console.log(product));
         console.log(this.data);
        this.dialogRef.close(this.data);
    }
  }

}
