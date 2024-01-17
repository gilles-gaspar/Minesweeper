using Minesweeper.Server.Model;

namespace Minesweeper.Server.Services
{
    public interface IMineSweeperService
    {
        public Board GetBoard();
        public void SartNewGame(int rowCount, int columnCount, int mineCount);
        public void RevealCell(int row, int col);
        public void ToggleFlagCell(int row, int col);
    }
}
