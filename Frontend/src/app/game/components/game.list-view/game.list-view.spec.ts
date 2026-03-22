import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameListView } from './game.list-view';

describe('GameListView', () => {
  let component: GameListView;
  let fixture: ComponentFixture<GameListView>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GameListView],
    }).compileComponents();

    fixture = TestBed.createComponent(GameListView);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
