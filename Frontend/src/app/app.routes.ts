import { Routes } from '@angular/router';
import { HomeComponent } from './game/home/home';
import { GameDetailsView } from './game/components/game.details-view/game.details-view';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'game/:id',
    component: GameDetailsView,
  }
];
