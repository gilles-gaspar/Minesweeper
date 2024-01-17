import { FlagStatus } from "../enum";

export interface Cell {
    row:number;
    column:number;
    isMine:boolean;
    neighbourMines:number;
    isRevealed:boolean;
    isFlagged:FlagStatus;
  }