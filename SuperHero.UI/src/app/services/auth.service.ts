import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  user: any = null;

  loadUser(){
    const request = this.http.get<any>(`${environment.apiUrl}/user`)
    request.subscribe(user => this.user = user);
  }

  login(loginForm: any){
    return this.http.post<any>(`${environment.apiUrl}/login`, loginForm, {withCredentials: true})
      .subscribe(_=>{})
  }

  register(){

  }

  logout(){
    return this.http.get(`${environment.apiUrl}/logout`, {withCredentials: true})
      .subscribe(_=>this.user=null)
  }
}
