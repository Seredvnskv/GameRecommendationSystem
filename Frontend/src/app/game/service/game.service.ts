import { Injectable } from '@angular/core';
import {  GameDto } from '../dto/game.dto';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class GameService {
  constructor(private http: HttpClient) {}

  getGames(): Observable<GameDto[]>
  {
    return this.http.get<GameDto[]>(`/api/games`);
  }

  getGameById(id: string): Observable<GameDto>
  {
    return this.http.get<GameDto>(`/api/games/${id}`);
  }

  getGamesByTitle(title: string): Observable<GameDto[]>
  {
    return this.http.get<GameDto[]>(`/api/games/search?title=${title}`);
  }

  getRecommendedGames(id: string): Observable<GameDto[]>
  {
    return this.http.get<GameDto[]>(`/api/games/${id}/recommendations`);
  }
}
