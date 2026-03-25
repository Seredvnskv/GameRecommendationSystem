import { Component, Input } from '@angular/core';
import {GameDto} from '../../dto/game.dto';
import {Router} from '@angular/router';

@Component({
  selector: 'app-game-card',
  standalone: true,
  imports: [],
  templateUrl: './game.card.html',
  styleUrl: './game.card.css',
})
export class GameCard {
  constructor(private router: Router) {}
  @Input() game: GameDto | undefined;

  onClick(): void {
    if (this.game) {
      this.router.navigate(['/game', this.game.gameId]);
    }
  }
}
