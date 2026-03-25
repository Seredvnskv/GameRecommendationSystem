import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import {GameDto} from '../../dto/game.dto';
import {GameCard} from '../game.card/game.card';

@Component({
  selector: 'app-game-card-carousel',
  standalone: true,
  imports: [CommonModule, GameCard],
  templateUrl: './game.card-carousel.html',
  styleUrl: './game.card-carousel.css',
})
export class GameCardCarousel {
  @Input() games: GameDto[] = [];
  currentIndex = 0;

  nextSlide(): void {
    if (this.currentIndex < this.games.length - 1) {
      this.currentIndex++;
    }
    else {
      this.currentIndex = 0;
    }
  }

  prevSlide(): void {
    if (this.currentIndex > 0) {
      this.currentIndex--;
    }
    else {
      this.currentIndex = this.games.length - 1;
    }
  }
}
