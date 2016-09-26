using System;
namespace Maths3D
{

    static class Mathematics3D
    {


        const double toRADIANS = Math.PI / 180.0;
        public static Point3D RotateX(Point3D p3D, float degrees)
        {
            double radians = degrees * toRADIANS;
            double cosD = Math.Cos(radians);
            double sinD = Math.Sin(radians);
            double y = (p3D.Y * cosD) + (p3D.Z * sinD);
            double z = (p3D.Y * -sinD) + (p3D.Z * cosD);
            return new Point3D(p3D.X, y, z);
        }

        public static Point3D RotateY(Point3D p3D, float degrees)
        {
            double radians = degrees * toRADIANS;
            double cosD = Math.Cos(radians);
            double sinD = Math.Sin(radians);
            double x = (p3D.X * cosD) + (p3D.Z * sinD);
            double z = (p3D.X * -sinD) + (p3D.Z * cosD);
            return new Point3D(x, p3D.Y, z);
        }

        public static Point3D RotateZ(Point3D p3D, float degrees)
        {
            double radians = degrees * toRADIANS;
            double cosD = Math.Cos(radians);
            double sinD = Math.Sin(radians);
            double x = (p3D.X * cosD) + (p3D.Y * sinD);
            double y = (p3D.X * -sinD) + (p3D.Y * cosD);
            return new Point3D(x, y, p3D.Z);
        }

        public static Point3D Translate(Point3D p3D, Point3D oldOrigin, Point3D newOrigin)
        {
            Point3D difference = new Point3D(newOrigin.X - oldOrigin.X, newOrigin.Y - oldOrigin.Y, newOrigin.Z - oldOrigin.Z);
            p3D.X += difference.X;
            p3D.Y += difference.Y;
            p3D.Z += difference.Z;
            return p3D;
        }

        public static Point3D[] RotateX(Point3D[] points3D, float degrees)
        {
            for (int i = 0; i <= points3D.Length - 1; i++)
            {
                points3D[i] = RotateX((Point3D)points3D[i], degrees);
            }
            return points3D;
        }

        public static Point3D[] RotateY(Point3D[] points3D, float degrees)
        {
            for (int i = 0; i <= points3D.Length - 1; i++)
            {
                points3D[i] = RotateY((Point3D)points3D[i], degrees);
            }
            return points3D;
        }

        public static Point3D[] RotateZ(Point3D[] points3D, float degrees)
        {
            for (int i = 0; i <= points3D.Length - 1; i++)
            {
                points3D[i] = RotateZ((Point3D)points3D[i], degrees);
            }
            return points3D;
        }

        public static Point3D[] Translate(Point3D[] points3D, Point3D oldOrigin, Point3D newOrigin)
        {
            for (int i = 0; i <= points3D.Length - 1; i++)
            {
                points3D[i] = Translate(points3D[i], oldOrigin, newOrigin);
            }
            return points3D;
        }

    }

}