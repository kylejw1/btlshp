using Battleship.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.View
{
    public abstract class ConsoleView 
    {
        public abstract void Draw();
        
        protected virtual void ConsoleDraw(IEnumerable<string> lines, int xOffset, int yOffset, ConsoleColor color)
        {
            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;
            var fgColor = Console.ForegroundColor;
            var cursorVisible = Console.CursorVisible;

            Console.ForegroundColor = color;
            Console.CursorVisible = false;

            foreach (var line in lines)
            {
                Console.SetCursorPosition(xOffset, yOffset++);
                Console.Out.Write(line);
            }

            // Restore previous values
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.ForegroundColor = fgColor;
            Console.CursorVisible = cursorVisible;
        }
    }
}
