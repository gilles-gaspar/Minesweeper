import { Component, Input } from '@angular/core';
import { MinesweeperService } from '../minesweeper.service';
import { FlagStatus } from '../enum';

@Component({
  selector: 'app-cell',
  templateUrl: './cell.component.html',
  styleUrl: './cell.component.css',
})
export class CellComponent {
  @Input() row: number = -1;
  @Input() column: number = -1;
  @Input() isMine: boolean = false;
  @Input() neighbourMines: number = -1;
  @Input() isRevealed: boolean = false;
  @Input() isFlagged: FlagStatus = 0;

  constructor(private _minesweeperSerice: MinesweeperService) {
  }

  onClick($event: MouseEvent) {
    this._minesweeperSerice.reveal( this.row, this.column);
  }

  onContextMenu($event: MouseEvent) {
    $event.preventDefault();
    this._minesweeperSerice.toggleflag(this.row, this.column);
    return false;
  }
}
