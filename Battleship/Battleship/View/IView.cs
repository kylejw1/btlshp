using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.View
{
    interface IView
    {
        void SetMiss(int player, Coordinate coordinate);
        void SetHit(int player, Coordinate coordinate);
        void SetSunk(int sunkPlayer, IEnumerable<Coordinate> sunkShipCoords);
        void SetPristine(int player, Coordinate coordinate);

        void SetState(GameState state);

        void AppendMessage(string message);
    }
}
