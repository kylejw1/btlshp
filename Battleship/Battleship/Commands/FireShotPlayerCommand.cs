using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battleship.Commands
{
    public class FireShotPlayerCommand : IPlayerCommand
    {

        private readonly Coordinate _coordinate;
        private readonly GameBoard _gameBoard;

        public FireShotPlayerCommand(Coordinate coordinate, GameBoard targetGameBoard)
        {
            _coordinate = coordinate;
            _gameBoard = targetGameBoard;
        }

        public void Execute()
        {
            _gameBoard.IncomingShot(_coordinate);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
