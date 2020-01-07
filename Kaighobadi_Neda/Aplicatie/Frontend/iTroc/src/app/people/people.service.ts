import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from 'src/environments/environment'
import { LoginService } from '../login/login.service';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {
 private url:string=environment.apiUrl;
 private token:string=this.loginService.getToken();
  constructor(private http:HttpClient,private loginService:LoginService) { }

  public getPeople(pageSize:number, page:number, name:string):Observable<any>{
    let header = {
      headers: new HttpHeaders({
        'auth-token': this.token
      }),
      params:{
        size:pageSize.toString(),
        page:page.toString(),
        name:name
        
      }
    }
      return this.http.get<any>(this.url+'/user');
  }

  public getUser(id:string):Observable<any>{
    let header = {
      headers: new HttpHeaders({
        'auth-token': this.token
      })}
      return this.http.get<any>(this.url+'/user/'+id,header);
  }
}
