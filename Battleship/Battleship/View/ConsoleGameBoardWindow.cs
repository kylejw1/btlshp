using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.View
{
    public class ConsoleGameBoardWindow : ConsoleWindow
    {
        private int _x;
        private int _y;

        public ConsoleGameBoardWindow(int x, int y) : base(ConsoleViewTemplates.GameBoard)
        {
            _x = x;
            _y = y;
        }

        public void DrawShot(char character, Coordinate shotLocation)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;

            Console.SetCursorPosition(this._x + 6 + (4 * shotLocation.Col), this._y + 3 + (2*shotLocation.Row));
            Console.SetCursorPosition(left, top);
        }

        public override void Draw(int x = 0, int y = 0)
        {
            base.Draw(x + _x, y + _y);
        }
    }
}
