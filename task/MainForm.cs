using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace task
{
    public partial class MainForm : Form
    {
        private readonly Graphics Graph;
        private readonly Pen MyPen;
        private readonly SolidBrush MyBrush;
        private readonly Random Random;

        private const int N = 500;
        private List<int> x, y, vX, vY, P;
        
        public MainForm()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Graph.SmoothingMode = SmoothingMode.HighQuality;
            MyPen = new Pen(Color.Black);
            MyBrush = new SolidBrush(Color.Black);
            Random = new Random();

            x = new List<int>();
            y = new List<int>();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //PaintPistonModel();
            //PaintPiston();
            //PaintMolecules();
            PaintSnowfall();
        }

        private void PaintSnowfall()
        {
            Graph.DrawRectangle(MyPen, 1, 1, 100, 100);
            Graph.FillRectangle(MyBrush, 1, 1, 100, 100);
            
            var bounds = Screen.GetBounds(new Point(0, 0));
            var bmp = new Bitmap(bounds.Width, bounds.Height);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(2, 30, 0, 0, bmp.Size);
            
            var image = new Bitmap(bmp);
            var pixelColor = image.GetPixel(0, 0);

            MyPen.Color = pixelColor;
            MyBrush.Color = pixelColor;
            Graph.DrawRectangle(MyPen, 150, 150, 100, 100);
            Graph.FillRectangle(MyBrush, 150, 150, 100, 100);
        }
        
        Color GetPixelColor(Point point) {
            Color color;
            using (Bitmap bmp = new Bitmap(Width, Height)) {
                DrawToBitmap(bmp, new Rectangle(new Point(), Size));
                bmp.Save("test.png", System.Drawing.Imaging.ImageFormat.Png);
                color = bmp.GetPixel(point.X, point.Y);
            }
            return color;
        }

        private void PaintPistonModel()
        {
            var xCenter = ClientSize.Width / 2;
            var yCenter = ClientSize.Height / 2;
            Graph.DrawRectangle(MyPen, xCenter - 300, yCenter - 150, 600, 300);
        }

        private void PaintPiston()
        {
            var xCenter = ClientSize.Width / 2;
            var yCenter = ClientSize.Height / 2;

            var width1 = 50;
            var height1 = 300;
            var width2 = 300;
            var height2 = 40;

            var xMax1 = xCenter + 250;
            var yMax1 = yCenter - 150;
            var xMax2 = xCenter + 300;
            var yMax2 = yCenter - 20;
            
            var xMin1 = xCenter - 200;
            var yMin1 = yCenter - 150;
            var xMin2 = xCenter - 150;
            var yMin2 = yCenter - 20;
            
            for (var i = 0; i < N; i++)
            {
                Graph.DrawRectangle(MyPen, xMax1, yMax1, width1, height1);
                Graph.DrawRectangle(MyPen, xMax2, yMax2, width2, height2);
                
                Graph.DrawRectangle(MyPen, xMin1, yMin1, width1, height1);
                Graph.DrawRectangle(MyPen, xMin2, yMin2, width2, height2);
            }
        }

        private void PaintMolecules()
        {
            var xMax = ClientSize.Width;
            var yMax = ClientSize.Height;
            
            for (var i = 0; i < N; i++)
            {
                x.Add(Random.Next() % xMax);
                y.Add(Random.Next() % yMax);
            }

            for (var count = 0; count < N; count++)
            {
                for (var i = 0; i < N; i++)
                {
                    MyPen.Color = Color.WhiteSmoke;
                    MyBrush.Color = Color.WhiteSmoke;
                    Graph.DrawRectangle(MyPen, x[i], y[i], 2, 2);
                    Graph.FillRectangle(MyBrush, x[i], y[i], 2, 2);
                    
                    var dX = -20 + Random.Next() % 41;
                    var dY = -10 + Random.Next() % 41;

                    if (x[i] + dX > 0 && x[i] + dX < xMax)
                    {
                        x[i] += dX;
                    }
                    if (y[i] + dY > 0 && y[i] + dY < yMax)
                    {
                        y[i] += dY;
                    }
                    
                    MyPen.Color = Color.Black;
                    MyBrush.Color = Color.Black;
                    Graph.DrawRectangle(MyPen, x[i], y[i], 2, 2);
                    Graph.FillRectangle(MyBrush, x[i], y[i], 2, 2);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Graph.Dispose();
            MyPen.Dispose();
        }
    }
}