import { Component, OnInit } from '@angular/core';
import { Board } from './board/iBoard';
import { BoardComponent } from './board/board.component';
import { MinesweeperService } from './minesweeper.service';
import { GameLevelEnum } from './enum';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  _level = new BehaviorSubject<GameLevelEnum>(GameLevelEnum.Easy);
  _board: Board;

  constructor(private _minesweeperSerice: MinesweeperService) {
    this._board = new BoardComponent();
  }

  ngOnInit() {
    this._minesweeperSerice.boardHasChanged.subscribe((board: Board) => {
      this._board = board;
    });
    this._level.subscribe((gameLevelSelected) => {
      if (gameLevelSelected === GameLevelEnum.Easy) {
        this._board.width = 9;
        this._board.height = 9;
        this._board.mineCount = 10;
      } else if (gameLevelSelected === GameLevelEnum.Medium) {
        this._board.width = 16;
        this._board.height = 16;
        this._board.mineCount = 40;
      } else if (gameLevelSelected === GameLevelEnum.Hard) {
        this._board.width = 16;
        this._board.height = 30;
        this._board.mineCount = 99;
      }
    });
    this.startNewGame();
  }

  startNewGame() {
    this._minesweeperSerice.startNewGame(
      this._board.width,
      this._board.height,
      this._board.mineCount
    );
  }

  onChangeGameLevel(levelSelected: GameLevelEnum): void {
    this._level.next(levelSelected);
    this.startNewGame();
  }

  title = 'Minesweeper';
}
