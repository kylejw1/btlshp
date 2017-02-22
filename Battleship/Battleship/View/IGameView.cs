using Battleship.Models;
using System.Collections.Generic;
using System.Drawing;

namespace Battleship.View
{
    interface IGameView
    {
        void Show();
        void SetFireResult(Player targetPlayer, Point coordinate, CellState result);
        void SetSunk(Player sunkPlayer);
        void Initialize(Player player1, Player player2);
        void SetShip(Player player2, IEnumerable<Point> shipPoints);
    }
}
