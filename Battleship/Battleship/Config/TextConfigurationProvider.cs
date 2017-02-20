using Battleship.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Config
{
    public class TextConfiguratonProvider : IConfigurationProvider
    {
        private TextReader _input;
        private TextWriter _output;

        /// <summary>
        /// Game configuration provider based on text streams.  Closure of streams is the responsibility of the caller.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public TextConfiguratonProvider(TextReader input, TextWriter output)
        {
            if (null == input || null == output)
            {
                throw new ArgumentNullException("Both input reader and output writer must be defined");
            }

            _input = input;
            _output = output;
        }

        public Configuration GetConfiguration()
        {
            DrawTitle(_output);

            _output.WriteLine();

            _output.WriteLine("Please enter name for Player 1.");
            var p1 = _input.ReadLine();

            _output.WriteLine("Please enter name for Player 2.");
            var p2 = _input.ReadLine();

            return new Configuration(p1, p2);
        }

        private void DrawTitle(TextWriter _output)
        {
            var initialColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;

            var title = TextViewTemplates.Title;

            title.ForEach(line => _output.WriteLine(line));

            Console.ForegroundColor = initialColor;
        }
    }
}
