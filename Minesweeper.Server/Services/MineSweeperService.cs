using Minesweeper.Server.Model;

namespace Minesweeper.Server.Services
{
    public class MineSweeperService : IMineSweeperService
    {
        private Board _board { get; set; }

        public MineSweeperService()
        {
        }

        public Board GetBoard()
        {
            if (_board == null)
                throw new NullReferenceException("Board not initialized");

            return _board;
        }

        public void SartNewGame(int rowCount = 15, int columnCount = 15, int mineCount = 15)
        {
            _board = new Board(rowCount, columnCount, mineCount);
        }

        public void RevealCell(int row, int col)
        {
            _board.RevealCell(row, col);
        }

        public void ToggleFlagCell(int row, int col)
        {
            _board.ToggleFlagCell(row, col);
        }
    }
}
