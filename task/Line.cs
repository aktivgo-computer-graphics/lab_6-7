using System;
using System.Drawing;

namespace task
{
    public class Line
    {
        private Point a, b;
        
        public Line(Point a, Point b)
        {
            if (a.X <= b.X)
            {
                this.a = a;
                this.b = b;
            }
            else
            {
                this.a = b;
                this.b = a;
            }
        }

        public bool IsPointOwned(Point p)
        {
            var k = (a.Y - b.Y) / (a.X - b.X);
            var offset = a.Y - a.X * k;
            return p.Y == k * p.X + offset;
        }
    }
}