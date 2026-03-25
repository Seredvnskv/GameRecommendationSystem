import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameCardCarousel } from './game.card-carousel';

describe('GameCardCarousel', () => {
  let component: GameCardCarousel;
  let fixture: ComponentFixture<GameCardCarousel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GameCardCarousel],
    }).compileComponents();

    fixture = TestBed.createComponent(GameCardCarousel);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
