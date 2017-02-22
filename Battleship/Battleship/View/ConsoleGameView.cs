using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship.View
{
    public class ConsoleGameView : IGameView
    {
        private int WindowWidth = 100;
        private int WindowHeight = 32;

        private Dictionary<Player, ConsoleGameBoardView> _views = new Dictionary<Player, ConsoleGameBoardView>();

        private int _cursorTop;
        private int _cursorLeft;

        /// <summary>
        /// Initialize the view and create any subviews if necessary
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void Initialize(Player player1, Player player2)
        {
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, Console.BufferHeight);

            var board1 = new ConsoleGameBoardView(player1.Name, 0, 0);
            var board2 = new ConsoleGameBoardView(player2.Name, board1.Width + 10, 0);
            _views[player1] = board1;
            _views[player2] = board2;

            _cursorLeft = 0;
            _cursorTop = _views.Values.Max(v => v.Height) + 2;
        }

        /// <summary>
        /// Display the game with game boards
        /// </summary>
        public void Show()
        {
            Console.Clear();
            _views.Values.ToList().ForEach(v => v.Draw());
            Console.SetCursorPosition(_cursorLeft, _cursorTop);
        }

        public void SetFireResult(Player targetPlayer, Point coordinate, CellState result)
        {
            if (result == CellState.Hit)
            {
                _views[targetPlayer].SetHit(coordinate);
            }
            else
            {
                _views[targetPlayer].SetMiss(coordinate);
            }
            Show();
        }

        public void SetSunk(Player sunkPlayer)
        {
            _views[sunkPlayer].SetSunk();
            Show();
        }

        public void SetShip(Player player, IEnumerable<Point> shipPoints)
        {
            _views[player].SetShip(shipPoints);
            Show();
        }
    }
}
