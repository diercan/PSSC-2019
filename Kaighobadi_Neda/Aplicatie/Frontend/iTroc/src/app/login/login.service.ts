import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../user';
import { UserLogin } from '../UserLogin';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
 
private url:string=environment.apiUrl+'/userLogins';
private rapidApiCountries="https://ajayakv-rest-countries-v1.p.rapidapi.com/rest/v1/all";
public token:string;
public country;
private rapidApiHeader={
  headers:new HttpHeaders({
    "x-rapidapi-host": "ajayakv-rest-countries-v1.p.rapidapi.com",
		"x-rapidapi-key": "bbeda2ef76msh1f27186b9018c88p148e73jsn41f494598403"
  })
}
  constructor(private http: HttpClient) { 
    this.token=this.getToken();
  }
  creationHeader = {
    headers: new HttpHeaders({
      'auth-token': this.token
    })
  }
  getUser(): any {
    return JSON.parse(localStorage.getItem('user'));
  }
  setUser(user: any): void {
    localStorage.setItem('user', JSON.stringify(user));
  }
  getToken(): string {
    return localStorage.getItem('token');
  }

  setToken(token: string): void {
    localStorage.setItem('token', token);
  }
  getLogin(user: UserLogin): Observable<any> {
    let header = {
      headers: new HttpHeaders({
        'DoNotIntercept': ''
      })
    };
    return this.http.post<any>(this.url+'/login',user);
  }
  getLogout(): Observable<any> {
    return this.http.get<any>(environment.apiUrl+'/logout', this.creationHeader)
  }
  register( user: User):Observable<any>{
    return this.http.post<any>(this.url+"/register",user);
  }
  getCountries():Observable<any>{
    return this.http.get<any>(this.rapidApiCountries,this.rapidApiHeader);
  }
}
