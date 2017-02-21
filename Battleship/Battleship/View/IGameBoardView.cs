using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.View
{
    public interface IGameBoardView
    {
        void SetMiss(Point coordinate);
        void SetHit(Point coordinate);
        void SetPristine(Point coordinate);
    }
}
