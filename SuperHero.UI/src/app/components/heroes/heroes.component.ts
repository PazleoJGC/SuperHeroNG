import { Component } from '@angular/core';
import { SuperHero } from 'src/app/models/super-hero';
import { AuthService } from 'src/app/services/auth.service';
import { SuperHeroService } from 'src/app/services/super-hero.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent {
  heroes: SuperHero[] = [];

  heroToEdit?: SuperHero

  constructor(protected auth: AuthService, private superHeroService: SuperHeroService) {}

  ngOnInit() : void {
    this.superHeroService
      .getSuperHeroes()
      .subscribe((result: SuperHero[]) => (this.heroes = result));
    console.log(this.heroes);
  }

  updateHeroList(heroes: SuperHero[]){
    this.heroes = heroes;
    this.heroToEdit = undefined;
  }

  initNewHero(){
    this.heroToEdit = new SuperHero();
  }

  editHero(hero: SuperHero){
    //shallow copy to disconnect the object from its table entry
    this.heroToEdit = Object.assign({}, hero);
  }
}
