using System;
using System.Linq;

namespace Battleship.View
{
    public class ConsoleWindow
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        protected string _template;

        public ConsoleWindow(string template)
        {
            _template = template;

            int width, height;
            ConsoleViewTemplates.GetTemplateDimensions(template, out width, out height);
            Width = width;
            Height = height;
        }

        public virtual void Draw(int x=0, int y=0)
        {
            DrawImage(_template, x, y);
        }

        protected void DrawImage(string template, int x, int y)
        {
            var lines = template.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Take(Height);

            foreach (var line in lines)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write(line.Substring(0, Width < line.Length ? Width : line.Length));
            }
        }
    }
}
