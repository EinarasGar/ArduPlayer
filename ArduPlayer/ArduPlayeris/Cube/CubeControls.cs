using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;
using System;
using ArduPlayeris.LedCube;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using ArduPlayeris.Cube.Animations;

namespace ArduPlayeris
{
    public delegate void LedsListener(List<int> litLeds);
    public delegate void LedListener(int led, bool isGlowing);
    partial class MainForm
    {
        private LedCube.Cube mainCube;
        private Point drawOrigin;
        public List<int> LitLeds = new List<int>();
        Point centerPos = new Point();
        int lastX = 0;
        int lastY = 0;
        bool limit = false;
        int scroll = 5;

        /// <summary>
        /// List<int> 
        /// </summary>
        public event LedsListener LedsChanged;

        /// <summary>
        /// int litLed
        /// bool on/off 
        /// </summary>
        public event LedListener LedStateChanged;

        private void MetroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl1.SelectedIndex == 3)
            {                
                this.KeyPreview = true;
                
                mainCube = new LedCube.Cube(200);

                drawOrigin = new Point(CubePictureBox.Width / 2, CubePictureBox.Height / 2);
                centerPos.X = CubePictureBox.Width / 2;
                centerPos.Y = CubePictureBox.Height / 2;
                RenderCube();
                this.MouseWheel += MainForm_MouseWheel;
            }
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                mainCube = new LedCube.Cube(mainCube.height + scroll);
            }
            else
            {
                mainCube = new LedCube.Cube(mainCube.height - scroll);

            }
            RenderCube();
        }

        public void RenderCube()
        {
            if (mainCube == null)
                return;
            mainCube = new LedCube.Cube(mainCube.height);
            mainCube.RotateX = Convert.ToSingle(TrackBarX.Value);
            mainCube.RotateY = Convert.ToSingle(TrackBarY.Value);
            mainCube.RotateZ = Convert.ToSingle(0);
            mainCube.uzdegti = LitLeds;
            CubePictureBox.Image = mainCube.DrawCube(drawOrigin);
            CubePictureBox.Invalidate();
        }

        private void TackBarScroll(object sender, ScrollEventArgs e)
        {
            RenderCube();
        }
        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (limit) { limit = false; return; } else { limit = true; }

            if (e.Button.ToString() == "Left, Right")
            {
                int dx = e.X - lastX;
                int dy = e.Y - lastY;
                TrackBarX.Value -= dy;
                TrackBarY.Value -= dx;

                RenderCube();
                lastX = e.Location.X;
                lastY = e.Location.Y;
                return;
            }


            if (e.Button == MouseButtons.Middle)
            {
                int dx = e.X - lastX;
                int dy = e.Y - lastY;
                centerPos.Y += dy;
                centerPos.X += dx;
                drawOrigin = new Point(centerPos.X, centerPos.Y);
                RenderCube();
            }
            if (e.Button == MouseButtons.Left)
            {
                selectLed(e, true);
            }
            if (e.Button == MouseButtons.Right)
            {
                selectLed(e, false);
            }
            lastX = e.Location.X;
            lastY = e.Location.Y;
        }

        public static bool IsPointInPolygon4(PointF[] polygon, PointF testPoint) //http://stackoverflow.com/a/14998816/5257707
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectLed(e, true);
            }
            if (e.Button == MouseButtons.Right)
            {
                selectLed(e, false);
            }
        }

        void selectLed(MouseEventArgs e, bool onoff)
        {
            Dictionary<int, float> plotai = new Dictionary<int, float>();
            for (int i = 0; i < 125; i++)
            {
                for (int o = 0; o < 6; o++)
                {
                    PointF[] point = mainCube.leds[i, o];
                    Point mouseLoc = e.Location;
                    if (IsPointInPolygon4(point, e.Location))
                    {
                        float plotas = 0;
                        for (int p = 0; p < 6; p++)
                        {
                            PointF[] _point = mainCube.leds[i, p];
                            int u, j;                                   // http://stackoverflow.com/a/2432482/5257707
                            float area = 0;                             // Not using this as a seperate function, 
                            for (u = 0; u < _point.Length; u++)         // because it returns the same value.
                            {
                                j = (u + 1) % _point.Length;

                                area += _point[u].X * _point[j].Y;
                                area -= _point[u].Y * _point[j].X;
                            }

                            area /= 2;
                            float _plotas = area < 0 ? -area : area;
                            plotas += _plotas;
                        }
                        plotai.Add(i, plotas);
                        break;
                    }
                }
            }
            float didziausiasPlotas = 0;
            foreach (float plotas in plotai.Values)
            {
                if (didziausiasPlotas < plotas)
                    didziausiasPlotas = plotas;
            }
            if (didziausiasPlotas != 0)
            {
                int Ledas = plotai.FirstOrDefault(x => x.Value == didziausiasPlotas).Key;
                if (onoff)
                {
                    if (!LitLeds.Contains(Ledas))
                    {
                       LitLeds.Add(Ledas);
                       LedsChanged?.Invoke(LitLeds);
                       LedStateChanged?.Invoke(Ledas,true);
                       serial.Send("a"+Ledas.ToString());
                    }
                }
                else
                {
                    LitLeds.Remove(Ledas);
                    LedsChanged?.Invoke(LitLeds);
                    LedStateChanged?.Invoke(Ledas, false);
                    serial.Send("r" +Ledas.ToString());
                }
            }
            RenderCube();
        }

    }
}
