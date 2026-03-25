import { Component, signal, OnInit } from '@angular/core';
import { GameDto } from '../../dto/game.dto';
import { GameService } from '../../service/game.service';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {CommonModule} from '@angular/common';
import { GameCarouselComponent } from '../image-carousel/image-carousel';
import {GameCardCarousel} from '../game.card-carousel/game.card-carousel';

@Component({
  selector: 'app-game.details-view',
  imports: [
    CommonModule,
    GameCarouselComponent,
    GameCardCarousel
  ],
  templateUrl: './game.details-view.html',
  styleUrl: './game.details-view.css',
})

export class GameDetailsView implements OnInit
{
  constructor(private gameService: GameService, private router: Router, private route: ActivatedRoute) {}

  game = signal<GameDto | null>(null);
  screenshots = signal<string[] | undefined>([]);
  id = signal<string>('');
  recommendedGames = signal<GameDto[]>([]);
  isLoading = signal(false);
  clicked = signal(false);

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id.set(params['id']);

      this.gameService.getGameById(this.id()).subscribe(game =>
      {
        this.game.set(game);
        this.screenshots.set(game.screenshots);
        this.clicked = signal(false);
      });
    });
  }

  goBack(): void
  {
    this.router.navigate(['/']);
    this.clicked = signal(false);
  }

  getRecommendedGames(): void
  {
    this.isLoading.set(true);
    this.clicked = signal(true);

    this.gameService.getRecommendedGames(this.id()).subscribe(
      {
        next: (games) => {
          this.recommendedGames.set(games);
          this.isLoading.set(false);

          console.log(this.recommendedGames());
        },
        error: (err) => {
          console.error('Error fetching recommended games:', err);
          this.recommendedGames.set([]);
          this.isLoading.set(false);
        },
      }
    )
  }
}
