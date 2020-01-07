import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from "../shared/shared.module"
import { ProductsComponent } from './products.component';
import { ProductsRoutingModule } from './products-routing.module';


@NgModule({
    imports: [
    CommonModule,
     SharedModule,
     ProductsRoutingModule
    ],
    providers: [
  ],
  exports: [],
  declarations: [ProductsComponent],
  
  })
  export class ProductsModule { }