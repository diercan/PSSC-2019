import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from "../shared/shared.module"
import { PeopleComponent } from './people.component';
import{UserProfileComponent} from './user-profile/user-profile.component'
import { PeopleRoutingModule } from './people-routing.module';
import { CurrentUserProfileComponent } from './current-user-profile/current-user-profile.component';
import { AddProductComponent } from './user-profile/add-product/add-product.component';

@NgModule({
    imports: [
    CommonModule,
     SharedModule,
     PeopleRoutingModule
    ],
    providers: [
  ],
  exports: [],
  entryComponents:[AddProductComponent],
  declarations: [PeopleComponent,UserProfileComponent, CurrentUserProfileComponent, AddProductComponent],
  
  })
  export class PeopleModule { }