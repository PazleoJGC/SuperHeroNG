import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'SuperHero.UI';

  constructor(private auth: AuthService) {}

  isValidUser() : boolean{
    return this.auth.isValidUser()
  }

  logout(){
    return this.auth.logout();
  }
}
