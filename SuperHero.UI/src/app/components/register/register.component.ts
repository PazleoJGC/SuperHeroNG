import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private auth: AuthService){}
  
  username: string = "";
  password: string = "";
  passwordConfirm: string = "";

  register(){
    return this.auth.register({
      username: this.username,
      password: this.password,
      passwordConfirm: this.passwordConfirm
    })
  }
}
