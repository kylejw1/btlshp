using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Config;

namespace Battleship.Commands
{
    public interface IPlayerInterface
    {
        Coordinate GetFiringCoordinate(string playerName);

        IPlayerCommand CreatePlaceShipCommand(GameBoard gameBoard);
        IPlayerCommand CreateFireShotCommand(GameBoard targetGameBoard);

    }
}
