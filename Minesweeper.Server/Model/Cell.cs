namespace Minesweeper.Server.Model
{
    /// <summary>
    /// Cell containing a mine or information about neighbour mines
    /// </summary>
    /// <param name="row">row in the board</param>
    /// <param name="column">column in the board</param>
    public class Cell(int row, int column)
    {
        private bool _isMine;
        
        public bool IsMine
        {
            get { return _isMine && IsRevealed; }
            internal set { _isMine = value; }
        }

        public int Row { get; set; } = row;
        public int Column { get; set; } = column;
        public bool IsRevealed { get; set; }
        public int NeighbourMines { get; set; }
        public FlagStatus IsFlagged { get; set; }

        /// <summary>
        /// Change the flag of the cell
        /// </summary>
        public void ToggleFlagCell()
        {
            if (!this.IsRevealed)
            {
                switch (this.IsFlagged)
                {
                    case FlagStatus.Flag:
                        this.IsFlagged = FlagStatus.QuestionMark;
                        break;
                    case FlagStatus.NoFlag:
                        this.IsFlagged = FlagStatus.Flag;
                        break;
                    case FlagStatus.QuestionMark:
                        this.IsFlagged = FlagStatus.NoFlag;
                        break;
                }
            }
        }

        /// <summary>
        /// Reveal the information of the cell
        /// </summary>
        /// <exception cref="GameOverException">If there is a mine, game is over</exception>
        public void RevealCell()
        {
            if (this.IsFlagged == FlagStatus.Flag)
                return; //Flagged cells cannot be revealed

            this.IsRevealed = true;
            this.IsFlagged = FlagStatus.NoFlag; //Revealed cells cannot be flagged

            // If the cell is a mine, game over!
            if (this._isMine)
                throw new GameOverException();
        }
    }
}
