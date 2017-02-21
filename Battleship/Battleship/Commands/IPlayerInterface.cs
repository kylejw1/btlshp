using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Config;
using System.Drawing;

namespace Battleship.Commands
{
    public interface IPlayerInterface
    {
        Ship GetPlayerShip(Player player);
        Point GetFiringCoordinate(Player shooter);
        void DisplayError(string message);
        void FireResult(CellState state);
    }
}
