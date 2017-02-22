using Battleship.Config;
using System.Drawing;

namespace Battleship.Models
{
    public class GameBoard
    {
        public static readonly Rectangle EnclosingRectangle = new Rectangle(0, 0, ConfigVariables.GridCols, ConfigVariables.GridRows);
    }
}