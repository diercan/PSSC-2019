import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import { UserLogin } from '../UserLogin';
import { User } from '../user';
import { Adress } from '../adress';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
 public isSigningIn:boolean=true;
 public isSigningUp:boolean=false;
 public loginForm:FormGroup;
 public registerForm:FormGroup;
 public countries:any[];
  constructor(private loginService:LoginService,private formBuilder:FormBuilder,private router:Router) { }

  ngOnInit() {
    this.loginForm=this.formBuilder.group({
    username:['',Validators.required],
    password:['',Validators.required]
    });
    this.registerForm=this.formBuilder.group({
      name:['',Validators.required],
      phone:['',Validators.compose([Validators.pattern(/^\d{10}$/),Validators.required])],
      email:['',Validators.compose([Validators.email,Validators.required])],
      username:['',Validators.required],
      password:['',Validators.required],
      city:['',Validators.required],
      country:['',Validators.required],
      userType:['',Validators.required],
    });
    this.loginService.getCountries().subscribe(countries=> {this.countries=countries;
      console.log(countries);});

  }
public signIn(){
  if(this.isSigningIn){
    let userLoginInfo:UserLogin= new UserLogin(this.loginForm.value.username,this.loginForm.value.password);
    this.loginService.getLogin(userLoginInfo).subscribe(response=>
      {
        console.log(response);
        this.loginService.setUser(response);
        this.loginService.setToken(response.token);
        this.router.navigate(['/home']);
      });
  }
  if(this.isSigningUp==true){
    let isSeller:boolean;
    if(this.registerForm.value.userType=="seller"){
      isSeller=true;
    }
    else{
      isSeller=false;
    }
    if(this.registerForm.valid){
    let adress: Adress= new Adress(this.registerForm.value.city,this.registerForm.value.country);
    console.log(adress)
    let user= new User(this.registerForm.value.name,this.registerForm.value.phone, this.registerForm.value.email, this.registerForm.value.username,this.registerForm.value.password,adress,
      this.registerForm.value.isSeller);
    this.loginService.register(user).subscribe(response=>{console.log(response);
      console.log(user);
      this.register();
    
    })
  }
  } 
}
public register(){
this.isSigningUp=!this.isSigningUp;
this.isSigningIn=!this.isSigningIn; 
}
}
