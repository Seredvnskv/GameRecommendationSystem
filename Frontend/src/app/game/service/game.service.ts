import { Injectable } from '@angular/core';
import {  GameDto } from '../dto/game.dto';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class GameService {
  constructor(private http: HttpClient) {}

  GetGames(): Observable<GameDto[]>
  {
    return this.http.get<GameDto[]>(`/api/games`);
  }

  GetGameById(id: string): Observable<GameDto>
  {
    return this.http.get<GameDto>(`/api/games/${id}`);
  }

  GetGamesByTitle(title: string): Observable<GameDto[]>
  {
    return this.http.get<GameDto[]>(`/api/games/search?title=${title}`);
  }

  GetRecommendedGames(id: string): Observable<GameDto[]>
  {
    return this.http.get<GameDto[]>(`/api/games/${id}`);
  }
}
