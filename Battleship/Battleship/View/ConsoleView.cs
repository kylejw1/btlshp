using Battleship.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.View
{
    public class ConsoleView : IView
    {
        ILogger logger = Resolver.Resolve<ILogger>();

        private int WindowWidth, WindowHeight;
        private int InputX, InputY;

        private ConsoleWindow _mainWindow;
        private ConsoleWindow _titleWindow;
        private ConsoleWindow _sunkWindow;
        private ConsoleWindow _player1GameBoard;
        private ConsoleWindow _player2GameBoard;

        public ConsoleView()
        {
            // Initialize windows
            var gameBoardPaddingX = 2;
            var gameBoardPaddingY = 2;
            _mainWindow = new ConsoleWindow(ConsoleViewTemplates.MainWindow);
            _titleWindow = new ConsoleWindow(ConsoleViewTemplates.Title);
            _sunkWindow = new ConsoleWindow(ConsoleViewTemplates.Sunk);
            _player1GameBoard = new ConsoleGameBoardWindow(gameBoardPaddingX, gameBoardPaddingY);
            _player2GameBoard = new ConsoleGameBoardWindow(2 * gameBoardPaddingX + _player1GameBoard.Width, gameBoardPaddingY);

            // Add some buffer at the ends
            InputX = 2;
            InputY = _mainWindow.Height - 2;
            WindowWidth = _mainWindow.Width + 3;
            WindowHeight = _mainWindow.Height + 3;

            if (Console.LargestWindowWidth < WindowWidth || Console.LargestWindowHeight < WindowHeight)
            {
                dynamic logEvent = new LogEvent("Screen size too small to support Battleship");
                logEvent.MinWindth = WindowWidth;
                logEvent.MinHeight = WindowHeight;
                throw new LoggableException(logEvent);
            }

            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);
            Console.Clear();
        }

        public void AppendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void SetHit(int player, Coordinate coordinate)
        {
            //TODO BEEPS
            throw new NotImplementedException();
        }

        public void SetMiss(int player, Coordinate coordinate)
        {
            throw new NotImplementedException();
        }

        public void SetState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.MENU:
                    DrawMainMenuState();
                    break;
                case GameState.PLAY:
                    DrawPlayState();
                    break;
                case GameState.SUNK:
                    DrawSunkState();
                    break;

            }
        }

        public void SetSunk(int sunkPlayer, IEnumerable<Coordinate> sunkShipCoords)
        {
            //TODO BEEPS
            throw new NotImplementedException();
        }

        private void DrawSunkState()
        {
            // Don't redraw the main window
            _sunkWindow.Draw((_mainWindow.Width - _sunkWindow.Width) / 2, (_mainWindow.Height - _sunkWindow.Height) / 2);
        }

        private void DrawPlayState()
        {
            _mainWindow.Draw();
            _player1GameBoard.Draw();
            _player2GameBoard.Draw();
        }

        private void DrawMainMenuState()
        {
            _mainWindow.Draw();
            _titleWindow.Draw((_mainWindow.Width - _titleWindow.Width) / 2, (_mainWindow.Height - _titleWindow.Height) / 2);
        }

        public void SetPristine(int player, Coordinate coordinate)
        {
            throw new NotImplementedException();
        }

        private void DrawImage(string template, int x, int y)
        {
            foreach(var line in template.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                Console.SetCursorPosition(x, y++);
                Console.Write(line);
            }

            Console.SetCursorPosition(InputX, InputY);
        }
    }
}
