using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task
{
    public class Molecule
    {
        private static Random rand = new Random();
        private double R;
        private double Step;
        private PointF Location;
        private double DirectionRadians;
        private Pen pen;
        private double k;
        private double screenWidth, screenHeight;
        private bool toPiston;

        public static Molecule RandomGenerate(double leftX, double rightX, double downY, double upY, double R,
            double step, Pen pen, double scale, double screenWidth, double screenHeight)
        {
            Molecule molecule = new Molecule();
            molecule.screenWidth = screenWidth;
            molecule.screenHeight = screenHeight;
            molecule.k = scale;
            molecule.pen = pen;
            molecule.R = R;
            molecule.Step = step;
            molecule.Location = new PointF((float)(rand.NextDouble() * ((rightX - (2 * R + 4)) - (leftX + 2 * R)) + (leftX + 2 * R)),
                    (float)(rand.NextDouble() * ((upY - (2 * R)) - (downY + 2 * R)) + (downY + 2 * R)));
            molecule.DirectionRadians = rand.NextDouble() * 2 * Math.PI;
            return molecule;
        }

        public void Move(RectangleF bounds, Graphics graphics)
        {
            var draw = false;
            var val = Location.X - R - bounds.X;
            if (val < Step)
            {
                toPiston = draw = true;
                DirectionRadians = -DirectionRadians - Math.PI;
                Location = RotatePoint(Location, Step, DirectionRadians);
                DrawMolecule(graphics);
            }
            val = bounds.Y - (Location.Y + R);
            if (val < Step)
            {
                toPiston = draw = true;
                DirectionRadians = -DirectionRadians;
                Location = RotatePoint(Location, Step, DirectionRadians);
                DrawMolecule(graphics);
            }
            val = Location.Y - R - (bounds.Y - bounds.Height);
            if (val < Step)
            {
                toPiston = draw = true;
                DirectionRadians = -DirectionRadians;
                Location = RotatePoint(Location, Step, DirectionRadians);
                DrawMolecule(graphics);
            }
            val = bounds.X + bounds.Width - (Location.X + R);
            if (val < Step)
            {
                draw = true;
                if (val <= 0)
                {
                    Location.X = (float)(bounds.X + bounds.Width - R);
                }
                if (toPiston)
                {
                    DirectionRadians = -DirectionRadians + Math.PI;
                    toPiston = !toPiston;
                }
                Location = RotatePoint(Location, Step, DirectionRadians);
                if (Location.X + R > bounds.X + bounds.Width)
                {
                    Location.X = (float)(bounds.X + bounds.Width - R);
                }
                DrawMolecule(graphics);
            }

            if (draw) return;
            Location = RotatePoint(Location, Step, DirectionRadians);
            DrawMolecule(graphics);
        }

        private void DrawMolecule(Graphics graphics)
        {
            var leftUpPoint = ScreenCoords(X - R, Y + R);
            graphics.DrawEllipse(pen, leftUpPoint.X, leftUpPoint.Y, (float)(2 * R * k), (float)(2 * R * k));
        }

        private PointF ScreenCoords(double x, double y)
        {
            return new PointF((float)(screenWidth / 2 + x * k), (float)(screenHeight / 2 - y * k));
        }

        private PointF RotatePoint(PointF point, double radius, double angle)
        {
            return new PointF((float)(radius * Math.Cos(angle) + point.X),
                (float)(radius * Math.Sin(angle)) + point.Y);
        }

        private float X
        {
            get => Location.X;
            set => Location.X = value;
        }

        private float Y
        {
            get => Location.Y;
            set => Location.Y = value;
        }
    }
}
