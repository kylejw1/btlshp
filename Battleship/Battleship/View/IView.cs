using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.View
{
    interface IView
    {
        void SetMiss(int player, Point coordinate);
        void SetHit(int player, Point coordinate);
        void SetSunk(int sunkPlayer, IEnumerable<Point> sunkShipCoords);
        void SetPristine(int player, Point coordinate);

        void SetState(GameState state);

        void AppendMessage(string message);
    }
}
