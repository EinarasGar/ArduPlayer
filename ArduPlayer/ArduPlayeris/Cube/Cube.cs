using Maths3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace ArduPlayeris.LedCube
{
    class Cube
    {

        public int width = 0, height = 0, depth = 0;
        int counter = 0;

        private Face[] cubeFaces;
        public Face FrontFace, BackFace, LeftFace, RightFace, TopFace, BottomFace;
        private enum rotationAxis { X, Y, Z }
        private float _xRotation = 0f, _yRotation = 0f, _zRotation = 0f;


        private Point3D cubeOrigin;
        public PointF[,][] leds = new PointF[125, 6][];

        SolidBrush blue = new SolidBrush(Color.FromArgb(60, Color.Blue));
        SolidBrush gray = new SolidBrush(Color.FromArgb(30, Color.Gray));

        public List<int> uzdegti = new List<int>();


        public float RotateX
        {
            get { return _xRotation; }
            set
            {
                RotateCube(value - _xRotation, rotationAxis.X);
                _xRotation = value;
            }
        }

        public float RotateY
        {
            get { return _yRotation; }
            set
            {
                RotateCube(value - _yRotation, rotationAxis.Y);
                _yRotation = value;
            }
        }

        public float RotateZ
        {
            get { return _zRotation; }
            set
            {
                RotateCube(value - _zRotation, rotationAxis.Z);
                _zRotation = value;
            }
        }

        public Cube(int side)
        {
            Point3D origin = new Point3D(side / 2, side / 2, side / 2);
            width = side;
            height = side;
            depth = side;
            cubeOrigin = origin;
            InitializeCube();
        }

        public void InitializeCube()
        {
            FrontFace = new Face(0, 0, 0, 0, height, 0, width, height, 0, width, 0, 0);
            BackFace = new Face(0, 0, depth, 0, height, depth, width, height, depth, width, 0, depth);
            LeftFace = new Face(0, 0, 0, 0, 0, depth, 0, height, depth, 0, height, 0);
            RightFace = new Face(width, 0, 0, width, 0, height, width, height, depth, width, height, 0);
            TopFace = new Face(0, 0, 0, 0, 0, depth, width, 0, depth, width, 0, 0);
            BottomFace = new Face(0, height, 0, 0, height, depth, width, height, depth, width, height, 0);

            FrontFace.Center = new Point3D(width / 2, height / 2, 0);
            BackFace.Center = new Point3D(width / 2, height / 2, depth);
            LeftFace.Center = new Point3D(0, height / 2, depth / 2);
            RightFace.Center = new Point3D(width, height / 2, depth / 2);
            TopFace.Center = new Point3D(width / 2, 0, depth / 2);
            BottomFace.Center = new Point3D(width / 2, height, depth / 2);
            FrontFace.name = "FrontFace";
            BackFace.name = "BackFace";
            LeftFace.name = "LeftFace";
            RightFace.name = "RightFace";
            TopFace.name = "TopFace";
            BottomFace.name = "BottomFace";

            cubeFaces = new Face[6] {
                FrontFace,
                BackFace,
                LeftFace,
                RightFace,
                TopFace,
                BottomFace
            };

        }


        private void Update2DPoints(Point drawOrigin)
        {
            for (int i = 0; i <= cubeFaces.Length - 1; i++)
            {
                PointF[] point2D = new PointF[4];
                Point3D vector = default(Point3D);
                for (int o = 0; o <= point2D.Length - 1; o++)
                {
                    vector = cubeFaces[i].Corners3D[o];
                    point2D[o] = Get2D(vector, drawOrigin);
                }
                cubeFaces[i].Corners2D = point2D;
            }
        }


        private void RotateCube(float delta, rotationAxis axis)
        {
            for (int i = 0; i <= cubeFaces.Length - 1; i++)
            {
                //------Rotate points
                Point3D point0 = new Point3D(0, 0, 0);
                cubeFaces[i].Corners3D = Mathematics3D.Translate(cubeFaces[i].Corners3D, cubeOrigin, point0);
                switch (axis)
                {
                    case rotationAxis.X:
                        cubeFaces[i].Corners3D = Mathematics3D.RotateX(cubeFaces[i].Corners3D, delta);
                        break;
                    case rotationAxis.Y:
                        cubeFaces[i].Corners3D = Mathematics3D.RotateY(cubeFaces[i].Corners3D, delta);
                        break;
                    case rotationAxis.Z:
                        cubeFaces[i].Corners3D = Mathematics3D.RotateZ(cubeFaces[i].Corners3D, delta);
                        break;
                }
                cubeFaces[i].Corners3D = Mathematics3D.Translate(cubeFaces[i].Corners3D, point0, cubeOrigin);
                //-------Rotate center
                cubeFaces[i].Center = Mathematics3D.Translate(cubeFaces[i].Center, cubeOrigin, point0);
                switch (axis)
                {
                    case rotationAxis.X:
                        cubeFaces[i].Center = Mathematics3D.RotateX(cubeFaces[i].Center, delta);
                        break;
                    case rotationAxis.Y:
                        cubeFaces[i].Center = Mathematics3D.RotateY(cubeFaces[i].Center, delta);
                        break;
                    case rotationAxis.Z:
                        cubeFaces[i].Center = Mathematics3D.RotateZ(cubeFaces[i].Center, delta);
                        break;
                }
                cubeFaces[i].Center = Mathematics3D.Translate(cubeFaces[i].Center, point0, cubeOrigin);
            }
        }

        public Bitmap DrawCube(Point drawOrigin)
        {            
            Update2DPoints(drawOrigin);

            Bitmap finalBmp = new Bitmap(278, 278);

            using (Graphics g = Graphics.FromImage(finalBmp))
            {
                Dictionary<string, PointF[][]> faces = new Dictionary<string, PointF[][]>();
                g.Clear(Color.FromArgb(17, 17, 17));
                //sort faces from closets to farthest
                Array.Sort(cubeFaces);
                foreach (Face face in cubeFaces.Reverse())
                {
                    // if (face.name == "FrontFace" || face.name == "BackFace")
                    if (true)
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        PointF[] points2 = new PointF[14];
                        PointF[] points4 = new PointF[14];
                        float DiffX1 = (face.Corners2D[1].X - face.Corners2D[2].X) / 13;
                        float DiffY1 = (face.Corners2D[1].Y - face.Corners2D[2].Y) / 13;
                        float DiffX2 = (face.Corners2D[3].X - face.Corners2D[0].X) / 13;
                        float DiffY2 = (face.Corners2D[3].Y - face.Corners2D[0].Y) / 13;

                        PointF starting1 = face.Corners2D[1];
                        PointF starting2 = face.Corners2D[3];
                        for (int i = 0; i < 14; i++)
                        {
                            points2[i] = starting1;
                            starting1.X -= DiffX1;
                            starting1.Y -= DiffY1;

                            points4[i] = starting2;
                            starting2.X -= DiffX2;
                            starting2.Y -= DiffY2;
                        }

                        PointF[][] Grid = new PointF[14][];

                        for (int i = 0; i < 14; i++)
                        {
                            Grid[i] = CutLine(points2[i], points4[13 - i]);
                        }

                        using (Pen pn = new Pen(Color.Black, 1.8f))
                        {
                            g.DrawLine(pn, face.Corners2D[0], face.Corners2D[1]);
                            g.DrawLine(pn, face.Corners2D[1], face.Corners2D[2]);
                            g.DrawLine(pn, face.Corners2D[2], face.Corners2D[3]);
                            g.DrawLine(pn, face.Corners2D[3], face.Corners2D[0]);
                        }
                        faces.Add(face.ToString(), Grid);

                    }
                }

                PointF[,,] array3Da = new PointF[14, 14, 14];
                for (int i = 0; i < 14; i++)
                {
                    for (int o = 0; o < 14; o++)
                    {
                        PointF[] points = CutLine(faces["FrontFace"][i][o], faces["BackFace"][i][o]);
                        for (int p = 0; p < 14; p++)
                        {
                            array3Da[o, i, p] = points[p];
                        }
                    }
                }





                // Todo: Sort led cubes areas and then draw them from highest to lowest to prevent overlapping.

                for (int i = 12; i >= 0; i -= 3)
                {
                    for (int o = 12; o >= 0; o -= 3)
                    {
                        for (int p = 12; p >= 0; p -= 3)
                        {
                            drawLED(g, array3Da, i, o, p);
                        }
                    }
                }


                Dictionary<int, float> plotai = new Dictionary<int, float>();
                for (int i = 0; i < 125; i++)
                {
                    for (int o = 0; o < 6; o++)
                    {
                        PointF[] point = leds[i, o];
                        float plotas = 0;
                        for (int p = 0; p < 6; p++)
                        {
                            PointF[] _point = leds[i, p];
                            int u, j; // http://stackoverflow.com/a/2432482/5257707
                            float area = 0; // Not using this as a seperate function, 
                            for (u = 0; u < _point.Length; u++) // because it returns the same value.
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

                var plotai2 = from entry in plotai orderby entry.Value ascending select entry;

                foreach (KeyValuePair<int, float> pair in plotai2)
                {
                    int led = pair.Key;
                    for (int i = 0; i < 6; i++)
                    {
                        if (uzdegti.Contains(led))
                        {

                            g.FillPolygon(blue, leds[led, i]);
                        }
                        else
                        {
                            g.FillPolygon(gray, leds[led,i]);
                        }

                    }

                }

             
            }
            return finalBmp;
        }


     

        void drawLED(Graphics g, PointF[,,] array3Da, int layer, int y, int x)
        {
            PointF[][] point2D = new PointF[][]{
                new PointF[4] { array3Da[layer,      x, y], array3Da[layer,     x, y+1], array3Da[layer,   x+1, y+1], array3Da[layer,     x+1, y] }, //bottom
                new PointF[4] { array3Da[layer + 1, x, y], array3Da[layer + 1, x, y + 1], array3Da[layer + 1, x + 1, y + 1], array3Da[layer + 1, x + 1, y] }, //right
                new PointF[4] { array3Da[layer, x, y], array3Da[layer + 1, x, y], array3Da[layer + 1, x, y + 1], array3Da[layer, x, y + 1] },//top
                new PointF[4] { array3Da[layer, x, y], array3Da[layer + 1, x, y], array3Da[layer + 1, x + 1, y], array3Da[layer, x + 1, y] }, // left
                new PointF[4] { array3Da[layer + 1, x + 1, y + 1], array3Da[layer, x + 1, y + 1], array3Da[layer, x, y + 1], array3Da[layer + 1, x, y + 1] }, //front
                new PointF[4] { array3Da[layer + 1, x + 1, y + 1], array3Da[layer, x + 1, y + 1], array3Da[layer, x + 1, y], array3Da[layer + 1, x + 1, y] } // back
            };

            for (int i = 0; i < 6; i++)
            {
                leds[counter, i] = point2D[i];
                if (uzdegti.Contains(counter))
                {
                  //  g.FillPolygon(blue, point2D[i]);
                }
                else {
                  //  g.FillPolygon(gray, point2D[i]);
                }
            }
            counter++;
        }

        private PointF[] CutLine(PointF point1, PointF point2)
        {
            PointF[] points = new PointF[14];
            PointF starting = point1;
            float diffX = (point1.X - point2.X) / 13;
            float diffY = (point1.Y - point2.Y) / 13;
            for (int i = 0; i < 14; i++)
            {
                points[i] = starting;
                starting.X -= diffX;
                starting.Y -= diffY;
            }
            return points;
        }

        //Converts 3D points to 2D points
        private PointF Get2D(Point3D p3D, Point drawOrigin)
        {
            PointF returnPoint = new PointF();

            float zoom = Convert.ToSingle(Screen.PrimaryScreen.Bounds.Width) / 1.5f;
            Point3D tempCam = new Point3D(cubeOrigin.X, cubeOrigin.Y, (cubeOrigin.X * zoom) / cubeOrigin.X);
     
            float zValue = -p3D.Z - tempCam.Z;

            returnPoint.X = (tempCam.X - p3D.X) / zValue * zoom;
            returnPoint.Y = (tempCam.Y - p3D.Y) / zValue * zoom;

            return new PointF(returnPoint.X + drawOrigin.X, returnPoint.Y + drawOrigin.Y);
        }
    }
}

