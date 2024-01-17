namespace Minesweeper.Server
{
    public class GameOverException : Exception
    {
        public GameOverException()
            : base("You lose! Game over")
        {
        }
    }
}
