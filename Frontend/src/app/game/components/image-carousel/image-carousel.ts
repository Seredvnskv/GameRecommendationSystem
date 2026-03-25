import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-game-carousel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './image-carousel.html',
  styleUrls: ['./image-carousel.css'],
})
export class GameCarouselComponent {
  @Input() screenshots: string[] = [];
  currentIndex = 0;

  goToSlide(index: number) {
    this.currentIndex = index;
  }
}

