import { Cell } from "../cell/iCell";

export interface Board {
    mineCount:number;
    width:number;
    height:number;
    cells: Cell[][];
    status: 'inProgress' | 'failed' | 'completed';
    remainingMines:number;
}
