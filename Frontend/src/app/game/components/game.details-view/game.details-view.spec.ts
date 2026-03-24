import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameDetailsView } from './game.details-view';

describe('GameDetailsView', () => {
  let component: GameDetailsView;
  let fixture: ComponentFixture<GameDetailsView>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GameDetailsView],
    }).compileComponents();

    fixture = TestBed.createComponent(GameDetailsView);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
