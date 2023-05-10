import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'SuperHero.UI';

  username: string = "";

  constructor(private auth: AuthService) {}

  async ngOnInit(){
    await this.auth.loadUser();
  }

  getUserName() : string{
    return this.auth.user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
  }

  isValidUser() : boolean{
    return this.auth.isValidUser()
  }

  logout(){
    return this.auth.logout();
  }
}
