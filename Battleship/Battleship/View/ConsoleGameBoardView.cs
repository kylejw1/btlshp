using Battleship.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.View
{
    public class ConsoleGameBoardView : ConsoleView, IGameBoardView
    {
        private int _xOffset;
        private int _yOffset;

        public ConsoleGameBoardView(string playerName, int xOffset, int yOffset)
        {
            _xOffset = xOffset;
            _yOffset = yOffset;
            Template[0] = playerName;
        }

        public override void Draw()
        {
            ConsoleDraw(Template, _xOffset, _yOffset, ConsoleColor.White);
        }

        public List<string> Template = new List<string>() {
            "Player Name",
            @"+---+---+---+---+---+---+---+---+---+",
            @"|   | A | B | C | D | E | F | G | H |",
            @"+---+---+---+---+---+---+---+---+---+",
            @"| 1 |   |   |   |   |   |   |   |   |",
            @"+---+---+---+---+---+---+---+---+---+",
            @"| 2 |   |   |   |   |   |   |   |   |",
            @"+---+---+---+---+---+---+---+---+---+",
            @"| 3 |   |   |   |   |   |   |   |   |",
            @"+---+---+---+---+---+---+---+---+---+",
            @"| 4 |   |   |   |   |   |   |   |   |",
            @"+---+---+---+---+---+---+---+---+---+",
            @"| 5 |   |   |   |   |   |   |   |   |",
            @"+---+---+---+---+---+---+---+---+---+",
            @"| 6 |   |   |   |   |   |   |   |   |",
            @"+---+---+---+---+---+---+---+---+---+",
            @"| 7 |   |   |   |   |   |   |   |   |",
            @"+---+---+---+---+---+---+---+---+---+",
            @"| 8 |   |   |   |   |   |   |   |   |",
            @"+---+---+---+---+---+---+---+---+---+"
        };

        private void DrawPoint(Point p, char value, ConsoleColor color)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= ConfigVariables.GridCols || p.Y > ConfigVariables.GridRows)
            {
                //TODO Log warning
                return;
            }

            int x = (4 * p.X) + 6;
            int y = (2 * p.Y) + 4;

            ConsoleDraw(new string[] { value.ToString() }, _xOffset + x, _yOffset + y, color);
        }

        public void SetHit(Point coordinate)
        {
            DrawPoint(coordinate, 'X', ConsoleColor.Red);
        }

        public void SetMiss(Point coordinate)
        {
            DrawPoint(coordinate, '~', ConsoleColor.Blue);
        }

        public void SetPristine(Point coordinate)
        {
            DrawPoint(coordinate, ' ', ConsoleColor.Black);
        }
    }
}
