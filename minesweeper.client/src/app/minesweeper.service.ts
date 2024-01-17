import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Board } from './board/iBoard';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class MinesweeperService {
  private _boardData$ = new Subject<Board>();
  private API_URL = environment.API_URL;

  constructor(private http: HttpClient) {}

  get boardHasChanged$(): Observable<Board> {
    return this._boardData$.asObservable();
  }
  startNewGame(row: number, column: number, mines: number) {
    this.http
      .post(
        this.API_URL +
          `/api/Minesweeper/startNewGame/${row}/${column}/${mines}`,
        null
      )
      .subscribe({
        next: (v) => console.log('startNewGame:' + v),
        error: (e) => console.error(e),
        complete: () => this.getBoard(),
      });
  }

  getBoard() {
    this.http.get<Board>(this.API_URL + '/api/Minesweeper/board/').subscribe({
      next: (v) => {
        this._boardData$.next(v);
        console.info(this._boardData$);
      },
      error: (e) => console.error(e),
      complete: () => console.info('getBoard'),
    });
  }

  reveal(row: number, column: number) {
    this.http
      .post(this.API_URL + `/api/Minesweeper/reveal/${row}/${column}`, null)
      .subscribe({
        next: (v) => console.log(v),
        error: (e) => console.error(e),
        complete: () => this.getBoard(),
      });
  }

  toggleflag(row: number, column: number) {
    this.http
      .post(this.API_URL + `/api/Minesweeper/toggleflag/${row}/${column}`, null)
      .subscribe({
        next: (v) => console.log(v),
        error: (e) => console.error(e),
        complete: () => this.getBoard(),
      });
  }
}
