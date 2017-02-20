using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Config
{
    public class Configuration
    {
        public readonly string Player1Name;
        public readonly string Player2Name;

        public Configuration(string player1Name, string player2Name)
        {
            Player1Name = string.IsNullOrWhiteSpace(player1Name) ? "Player1" : player1Name;
            Player2Name = string.IsNullOrWhiteSpace(player2Name) ? "Player2" : player2Name;
        }
    }
}
