using Battleship.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship.View
{
    public class ConsoleGameBoardView
    {
        private int _xOffset;
        private int _yOffset;

        private List<Point> _hits = new List<Point>();
        private List<Point> _misses = new List<Point>();
        private List<Point> _shipPoints = new List<Point>();
        private bool _sunk = false;

        /// <summary>
        /// A small view for one battleship grid
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        public ConsoleGameBoardView(string playerName, int xOffset, int yOffset)
        {
            _xOffset = xOffset;
            _yOffset = yOffset;
            Template[0] = playerName;
        }

        /// <summary>
        /// Draw the grid to console
        /// </summary>
        public void Draw()
        {
            ConsoleDraw(Template, _xOffset, _yOffset, ConsoleColor.White);

            //Ship drawn before fire attempts
            _shipPoints.ForEach(s => DrawPoint(s, '#', ConsoleColor.Green));
            _misses.ForEach(m => DrawPoint(m, '~', ConsoleColor.Blue));
            _hits.ForEach(h => DrawPoint(h, 'X', ConsoleColor.Red));

            if (_sunk)
            {
                ConsoleDraw(SunkText, _xOffset + 3, _yOffset + 5, ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Draw the grid to console and return the cursor when done
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <param name="color"></param>
        private void ConsoleDraw(IEnumerable<string> lines, int xOffset, int yOffset, ConsoleColor color)
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

        public void SetShip(IEnumerable<Point> shipPoints)
        {
            _shipPoints = shipPoints.ToList();
        }

        public int Width
        {
            get
            {
                return Template.Max(s => s.Length);
            }
        }

        public int Height
        {
            get
            {
                return Template.Count;
            }
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

        private List<string> SunkText = new List<string>() {
            @" ______________________________",
            @"|   _____ __  ___   ____ __ __ |",
            @"|  / ___// / / / | / / //_// / |",
            @"|  \__ \/ / / /  |/ / ,<  / /  |",
            @"| ___/ / /_/ / /|  / /| |/_/   |",
            @"|/____/\____/_/ |_/_/ |_(_)    |",
            @"|______________________________|"
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
            _hits.Add(coordinate);
        }

        public void SetMiss(Point coordinate)
        {
            _misses.Add(coordinate);
        }

        public void SetPristine(Point coordinate)
        {
            if (_hits.Contains(coordinate))
            {
                _hits.Remove(coordinate);
            }
            if (_misses.Contains(coordinate))
            {
                _misses.Remove(coordinate);
            }
        }

        public void SetSunk()
        {
            _sunk = true;
        }
    }
}
