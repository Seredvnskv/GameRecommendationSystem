import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GameService } from '../game/service/game.service';
import { GameDto } from '../game/dto/game.dto';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class HomeComponent {
  searchQuery = signal('');
  games = signal<GameDto[]>([]);
  isLoading = signal(false);
  showResults = signal(false);

  constructor(private gameService: GameService) {}

  onSearch(): void {
    const query = this.searchQuery().trim();
    if (query.length === 0) {
      this.showResults.set(false);
      return;
    }

    this.isLoading.set(true);

    this.gameService.GetGamesByTitle(query).subscribe({
      next: (data) => {
        //console.log('Games received:', data);

        this.games.set(data);
        this.showResults.set(true);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error fetching games:', error);
        this.isLoading.set(false);
        this.games.set([]);
      },
    });
  }

  onInputChange(): void {
    if (this.searchQuery().length === 0) {
      this.showResults.set(false);
    }
  }

  clearSearch(): void {
    this.searchQuery.set('');
    this.games.set([]);
    this.showResults.set(false);
  }
}

