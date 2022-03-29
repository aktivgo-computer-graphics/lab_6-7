using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace task
{
    public partial class MainForm : Form
    {
        private Graphics Graph;
        private Pen MyPen;
        private SolidBrush MyBrush;
        private Timer timer;
        
        private double width;
        private double height;
        private const float Step = 0.2f;
        private double k = 20, leftX = -15, rightX = 15, downY = -10, upY = 10, pistonLength = 30, pistonLeftX = 13;
        private bool pistonDirection = true;
        private List<Molecule> molecules;

        private const int N = 500, Fps = 144;
        
        public MainForm()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Graph.SmoothingMode = SmoothingMode.HighQuality;
            MyPen = new Pen(Color.Black, 2);
            MyBrush = new SolidBrush(Color.Black);
            
            width = ClientSize.Width;
            height = ClientSize.Height;
            molecules = new List<Molecule>();

            RandomGenerateMolecules(20);

            InitTimer(1000 / Fps);
        }
        
        private void RandomGenerateMolecules(int moleculesCount)
        {
            for (var i = 0; i < moleculesCount; i++)
            {
                molecules.Add(Molecule.RandomGenerate(leftX, rightX, downY, upY, 0.5, Step, MyPen, k, width, height));
            }
        }

        private void InitTimer(int interval)
        {
            timer = new Timer();
            timer.Interval = interval;
            timer.Tick += timer_tick;
            timer.Enabled = true;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            PaintBox(e.Graphics);
            PaintPiston(e.Graphics);
            MoveMolecules(e.Graphics);
        }
        
        private void PaintBox(Graphics graph)
        {
            graph.DrawLine(MyPen, ScreenCoords(leftX, upY), ScreenCoords(rightX, upY));
            graph.DrawLine(MyPen, ScreenCoords(rightX, upY), ScreenCoords(rightX, 1));
            graph.DrawLine(MyPen, ScreenCoords(rightX, -1), ScreenCoords(rightX, downY));
            graph.DrawLine(MyPen, ScreenCoords(rightX, downY), ScreenCoords(leftX, downY));
            graph.DrawLine(MyPen, ScreenCoords(leftX, downY), ScreenCoords(leftX, upY));
        }
        
        private void PaintPiston(Graphics graph)
        {
            graph.DrawLine(MyPen, ScreenCoords(pistonLeftX, downY), ScreenCoords(pistonLeftX, upY));
            graph.DrawLine(MyPen, ScreenCoords(pistonLeftX + 2, downY), ScreenCoords(pistonLeftX + 2, upY));
            graph.DrawLine(MyPen, ScreenCoords(pistonLeftX + 2, 1), ScreenCoords(pistonLeftX + 2 + pistonLength, 1));
            graph.DrawLine(MyPen, ScreenCoords(pistonLeftX + 2, -1), ScreenCoords(pistonLeftX + 2 + pistonLength, -1));
            graph.DrawLine(MyPen, ScreenCoords(pistonLeftX + 2 + pistonLength, -1), ScreenCoords(pistonLeftX + 2 + pistonLength, 1));
            if (pistonDirection)
            {
                if (pistonLeftX > leftX + 5)
                {
                    pistonLeftX -= Math.Min(pistonLeftX - leftX, Step / 2);
                }
                else
                {
                    pistonDirection = false;
                }
            }
            else
            {
                if (pistonLeftX < rightX - 2)
                {
                    pistonLeftX += Math.Min(rightX - pistonLeftX, Step / 2);
                }
                else
                {
                    pistonDirection = true;
                }
            }
        }
        
        private void MoveMolecules(Graphics graph)
        {
            foreach (var molecule in molecules)
            {
                molecule.Move(new RectangleF((float)leftX, (float)upY, (float)(pistonLeftX - leftX), (float)(upY - downY)), graph);
            }
        }
        
        private PointF ScreenCoords(double x, double y)
        {
            return new PointF((float)(width / 2 + x * k), (float)(height / 2 - y * k));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Graph.Dispose();
            MyPen.Dispose();
            MyBrush.Dispose();
            timer.Dispose();
        }
    }
}