import { Component, OnInit } from '@angular/core';
import { LoginService } from '../login/login.service';
import { User } from '../user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public user:any;
  constructor(private loginService:LoginService,private router:Router) { }

  ngOnInit() {
  if(this.loginService.getToken()){
    console.log("hello");
  }
  else{
    this.router.navigate(['/login']);
  }
  }

}
