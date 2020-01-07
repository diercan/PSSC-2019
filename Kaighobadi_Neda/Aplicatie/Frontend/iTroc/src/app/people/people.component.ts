import { Component, OnInit } from '@angular/core';
import {User} from 'src/app/user'
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LoginService } from '../login/login.service';
import { PeopleService } from './people.service';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.scss']
})
export class PeopleComponent implements OnInit {
 public users: any[]=[];
 public searchForm:FormGroup;
 page:number=0;
 size:number=10;

  constructor(private loginService:LoginService,private peopleService:PeopleService,public router:Router,private formBuilder: FormBuilder) {
   }

  ngOnInit() {
    this.searchForm=this.formBuilder.group({
      name:['']
    });
     this.getUsers(this.page,this.size,"");
  }
  navigateToUserProfile(userId:string){
  
    this.router.navigate(["people/",userId]);
    
    
}
public getUsers(page:number,size:number,nameSearch:string){
  this.peopleService.getPeople(size,page,nameSearch).subscribe(users=>{
  this.users=users;
  console.log(users);
  })

}
public searchUser(){
  this.getUsers(this.page,this.size,this.searchForm.value.name);
  console.log(this.searchForm.value.name)
}

}
