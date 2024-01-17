import { Component, OnInit } from '@angular/core';
import { Board } from './board/iBoard';
import { BoardComponent } from './board/board.component';
import { MinesweeperService } from './minesweeper.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  _board: Board;

  constructor(private _minesweeperSerice: MinesweeperService) {
    this._board = new BoardComponent();
  }

  ngOnInit() {
    this._minesweeperSerice.boardHasChanged$.subscribe((board: Board) => {
      this._board = board;
    });
    this._minesweeperSerice.startNewGame(5, 5, 5);
  }

  startNewGame(row: number, column: number, mines: number) {
    this._minesweeperSerice.startNewGame(5, 5, 5);
  }

  title = 'Minesweeper';
}
