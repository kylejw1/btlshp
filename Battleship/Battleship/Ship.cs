using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship
{
    public class Ship
    {
        public List<Point> _points { get; private set; }
        private static readonly int ShipLength = 3;

        public Ship(Point startPoint, Point endPoint)
        {
            _points = new List<Point>();
            _points.Add(startPoint);
            _points.Add(endPoint);

            if (startPoint.X == endPoint.X)
            {
                // Vertical orientation
                var min = Math.Min(startPoint.Y, endPoint.Y);
                var max = Math.Max(startPoint.Y, endPoint.Y);
                while (++min < max)
                {
                    _points.Add(new Point(startPoint.X, min));
                }
            }
            else
            {
                // Horizontal orientation
                var min = Math.Min(startPoint.X, endPoint.X);
                var max = Math.Max(startPoint.X, endPoint.X);
                while (++min < max)
                {
                    _points.Add(new Point(min, startPoint.Y));
                }
            }

            ValidateCoordinates();
        }

        private void ValidateCoordinates()
        {
            if (_points.Count != ShipLength)
            {
                throw new ArgumentOutOfRangeException(string.Format("Ship must be exactly {0} cells", ShipLength));
            }

            if (!Geometry.ColinearAboutAxes(_points))
            {
                throw new ArgumentException("Ship coordinates must be horizontal or vertical");
            }
        }

    }
}