import { Component, Input } from '@angular/core';
import { Cell } from '../cell/iCell';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrl: './board.component.css',
})
export class BoardComponent {
  @Input() mineCount: number = 5;
  @Input() width: number = 5;
  @Input() height: number = 5;
  @Input() cells: Cell[][] = [];
  @Input() status: 'inProgress' | 'failed' | 'completed' = 'inProgress';
  @Input() remainingMine: number = 5;
}
