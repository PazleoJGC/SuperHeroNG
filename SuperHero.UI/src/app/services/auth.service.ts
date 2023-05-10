import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {
    this.loadUser()
  }

  user: any = null;

  isValidUser() : boolean{
    return this.user != undefined && Object.keys(this.user).length > 0
  }

  loadUser(){
    const request = this.http.get<any>(`${environment.apiUrl}/user`, {withCredentials: true})
    request.subscribe(user => {
      console.log(user)
      this.user = user
    })
  }

  login(loginForm: any){
    return this.http.post<any>(`${environment.apiUrl}/login`, loginForm, {withCredentials: true})
      .subscribe(_ => this.loadUser())
  }

  register(){

  }

  logout(){
    return this.http.get(`${environment.apiUrl}/logout`, {withCredentials: true})
      .subscribe(_=>this.user=null)
  }
}
