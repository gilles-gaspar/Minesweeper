namespace Minesweeper.Server.Model
{
    /// <summary>
    /// Board game
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Number of mines in the board
        /// </summary>
        public int MineCount { get; set; }

        /// <summary>
        /// Number of rows in the board
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Number of columns in the board
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Cells in the board
        /// </summary>
        public Cell[][]? Cells
        {
            get
            {
                if (_cells == null) return null;

                Cell[][] result = new Cell[Width][];
                for (int i = 0; i < Width; i++)
                    result[i] = [.. _cells.Where(c => c.Row == i).OrderBy(c => c.Column)];

                return result;
            }
        }
        private List<Cell> _cells { get; set; }

        /// <summary>
        /// Status of the game
        /// </summary>
        public GameStatus Status { get; set; }

        /// <summary>
        /// Remaining mines from the flags
        /// </summary>
        public int RemainingMines
        {
            get
            {
                if (_cells == null) return 0;
                return MineCount - _cells.Where(c => c.IsFlagged == FlagStatus.Flag).Count();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="mines"></param>
        public Board(int rows, int columns, int mines)
        {
            this.Width = rows;
            this.Height = columns;
            this.MineCount = mines;
            this.Status = GameStatus.InProgress;

            this._cells = [];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    _cells.Add(new Cell(i, j));

            //Select random cells
            Random rand = new();
            var mineList = _cells.OrderBy(user => rand.Next());
            var mineSlots = mineList.Take(this.MineCount).ToList().Select(z => new { z.Row, z.Column });

            //Place the mines
            foreach (var mineCoord in mineSlots)
            {
                var cell = _cells.First(cell => cell.Row == mineCoord.Row && cell.Column == mineCoord.Column);
                cell.IsMine = true;
                var neighbors = GetNeighbors(cell);
                foreach (var neighbor in neighbors)
                {
                    neighbor.NeighbourMines += 1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void RevealCell(int row, int col)
        {
            if (this.Status == GameStatus.Failed)
                return;

            // Find the specified cell
            var selectedCell = this._cells.Single(c => c.Row == row && c.Column == col);
            try
            {
                selectedCell.RevealCell();
            }
            catch (GameOverException)
            {
                this.Status = GameStatus.Failed;
                RevealAll();
                return;
            }

            // If the panel is a zero, cascade reveal neighbors
            RevealZeros(selectedCell);

            // Check if this is the last move to be complete
            CheckComplete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void ToggleFlagCell(int row, int col)
        {
            if (this.Status == GameStatus.Failed)
                return;

            // Find the specified cell
            var selectedCell = this._cells.Single(c => c.Row == row && c.Column == col);
            selectedCell.ToggleFlagCell();
        }

        private List<Cell> GetNeighbors(Cell current)
        {
            var neighbors = this._cells.Where(c => (c.Row == (current.Row - 1) && c.Column >= current.Column - 1 && c.Column <= current.Column + 1)
                || (c.Row == (current.Row + 1) && c.Column >= current.Column - 1 && c.Column <= current.Column + 1)
                || (c.Row == current.Row && (c.Column == current.Column - 1 || c.Column == current.Column + 1)));
            return neighbors.ToList();
        }

        private void RevealZeros(Cell cell)
        {
            if (cell.NeighbourMines == 0)
            {
                var neighbors = GetNeighbors(cell).Where(panel => !panel.IsRevealed);
                foreach (var neighbor in neighbors)
                {
                    neighbor.IsRevealed = true;
                    RevealZeros(neighbor);
                }
            }
        }

        private void CheckComplete()
        {
            var emptyCells = this._cells.Where(c => !c.IsRevealed && !c.GetIsMine());
            if (!emptyCells.Any())
            {
                this.Status = GameStatus.Completed;
                RevealAll();
            }
        }

        private void RevealAll()
        {
            var cells = this._cells.Where(c => !c.IsRevealed && !c.GetIsMine() && c.IsFlagged == FlagStatus.Flag);
            foreach (var cell in cells)
                cell.IsFlagged = FlagStatus.WrongFlag;

            cells = this._cells.Where(c => !c.IsRevealed);
            foreach (var cell in cells)
                cell.IsRevealed = true;
        }
    }
}
