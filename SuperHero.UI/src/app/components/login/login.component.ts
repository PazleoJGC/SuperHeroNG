import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AuthService]
})
export class LoginComponent {
  constructor(private auth: AuthService){}
  
  username: string = "";
  password: string = "";

  login(){
    return this.auth.login({
      username: this.username,
      password: this.password
    })
  }
}
