import { Component, OnInit, ViewEncapsulation, ViewChild, Input, AfterViewInit } from '@angular/core';
import { LoginService } from 'src/app/login/login.service';
import { Router } from '@angular/router';
import { MatMenuTrigger, MatDialog } from '@angular/material';
import { HttpErrorResponse } from '@angular/common/http';
import { trigger } from '@angular/animations';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  @ViewChild('notificationTrigger',{static:true}) notificationTrigger: MatMenuTrigger;
  @ViewChild('menuTrigger',{static:true}) menuTrigger: MatMenuTrigger;
  @Input('activeTab') activeTab: number;
  public disableButtons: boolean;
  public buttons: boolean[] = [false, false, false];
  public notifications: Notification[]=[];
  public user:any;
  public companyLabel: string="no company";
  public company: string;
  public companyLogo: string;
  public display: boolean[]=[false,false];
  public menuOpen:boolean;
  public notLoading:boolean=true;
  public page:number=1;
  public pageSize:number=5;
  public disableSeeMoreButton:boolean=false;
  public hasUnviewedNotifications: boolean=false;
  public moreNotifications:Notification[]=[];
  public url:string="./assets/blank-profile.png"

  constructor(private loginService: LoginService, public router: Router,public dialog: MatDialog) {
    if(this.user){
      console.log(this.user.picURL);
    if (!this.user.picURL)
      {this.user.picURL = 'https://writedirection.com/website/wp-content/uploads/2016/09/blank-profile-picture-973460_960_720.png';}
    }
    else{
     this.disableButtons=true;
    }
  }
  ngOnInit() {
    this.buttons[this.activeTab] = true;
    this.user=this.loginService.getUser();
    this.company = localStorage.getItem('company');
    this.companyLogo = localStorage.getItem('company-logo');
    console.log(this.user);
   
    
    }
    public activateButton(index: number): void {
      this.buttons.fill(false);
      if (index != 3)
        this.buttons[index] = true; 
    }
 public logOut(){
   this.loginService.getLogout().subscribe(el=>{console.log(el);
  this.router.navigate(['/login'])}
   );
 }
 navigateToUserProfile(userId){
  let currentUser;
    currentUser=this.loginService.getUser();
    if(userId==currentUser._id){
      this.router.navigate(["/people/",userId]);
    }
    else{
    this.router.navigate(["people/",userId]);
    }
}
}
