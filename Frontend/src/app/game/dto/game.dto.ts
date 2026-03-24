export interface GameDto {
  gameId: string;
  title: string;
  releaseDate: string;
  aboutGame: string;
  shortDescription: string;
  description: string;
  price: number;
  headerImage: string;
  metacriticScore: number;
  positiveRatings: number;
  negativeRatings: number;

  developers?: string[];
  publishers?: string[];
  categories?: string[];
  genres?: string[];
  screenshots?: string[];
  tags?: string[];
}
