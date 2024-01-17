using Microsoft.AspNetCore.Mvc;
using Minesweeper.Server.Model;
using Minesweeper.Server.Services;

namespace Minesweeper.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MineSweeperController : ControllerBase
    {
        private readonly IMineSweeperService mineSweeperService;

        public MineSweeperController(IMineSweeperService mineSweeperService)
        {
            this.mineSweeperService = mineSweeperService;
        }

        /// <summary>
        /// Initialize a new board
        /// </summary>
        /// <param name="rows">rows of board</param>
        /// <param name="columns">columns of board</param>
        /// <param name="mines">mines in board</param>
        /// <returns>Ok</returns>
        [HttpPost("startNewGame/{rows}/{columns}/{mines}")]
        public ActionResult StartNewGame(int rows, int columns, int mines)
        {
            mineSweeperService.SartNewGame(rows, columns, mines);
            return Ok();
        }

        /// <summary>
        /// return the board
        /// </summary>
        /// <returns>board</returns>
        [HttpGet("board")]
        public ActionResult<Board> GetBoard()
        {
            return mineSweeperService.GetBoard();
        }

        /// <summary>
        /// Reveal a cell in the board
        /// </summary>
        /// <param name="row">row of the cell</param>
        /// <param name="column">column of the cell</param>
        /// <returns>Ok</returns>
        [HttpPost("reveal/{row}/{column}")]
        public ActionResult RevealCell(int row, int column)
        {
            mineSweeperService.RevealCell(row, column);
            return Ok();
        }

        /// <summary>
        /// (Un)Mark a cell with a flag or question mark
        /// </summary>
        /// <param name="row">row of the cell</param>
        /// <param name="column">column of the cell</param>
        /// <returns>Ok</returns>
        [HttpPost("toggleflag/{row}/{column}")]
        public ActionResult ToggleFlagCell(int row, int column)
        {
            mineSweeperService.ToggleFlagCell(row, column);
            return Ok();
        }
    }
}
